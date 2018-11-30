using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TOT.Business.Exceptions;
using TOT.Entities.TimeOffRequests;
using TOT.Interfaces;
using TOT.Dto.TimeOffRequests;
using Microsoft.AspNetCore.Identity;
using TOT.Entities.IdentityEntities;
using System.Linq;
using TOT.Entities.TimeOffPolicies;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace TOT.Business.Services
{
    public class TimeOffRequestService : BaseService
    {
        private readonly UserManager<User> _userManager;

        public TimeOffRequestService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            _userManager = userManager;
        }

        public async Task CreateAsync(TimeOffRequestDTO requestDTO, User user)
        {
            if (requestDTO == null)
            {
                throw new ArgumentNullException(nameof(requestDTO));
            }

            var entry = mapper.Map<TimeOffRequestDTO, TimeOffRequest>(requestDTO);

            entry.UserId = user.Id;
            entry.Policy = GetEmployeePositionTimeOffPolicyByTypeAndPosition((int)requestDTO.TypeId, user.PositionId);

            unitOfWork.TimeOffRequests.Create(entry);
            CreateTimeOffRequestApprovalsForRequest(entry, requestDTO.ApproversId);

            var firstApproval = entry.Approvals.FirstOrDefault();

            await SendNotificationAsync(firstApproval, entry, " requested your to approve request");

            await unitOfWork.SaveAsync();
        }

        public Task DeleteAsync(int id)
        {
            unitOfWork.TimeOffRequests.Delete(id);

            return unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(TimeOffRequestDTO requestDTO, User user)
        {
            if (requestDTO == null)
            {
                throw new ArgumentNullException(nameof(requestDTO));
            }

            var request = unitOfWork.TimeOffRequests.Get(requestDTO.Id);

            if (request == null)
            {
                throw new EntityNotFoundException<TimeOffRequest>(requestDTO.Id);
            }

            if (IfApprovedAtLeastOnce(requestDTO.Id))
            {
                request.Note = requestDTO.Note;
            }
            else
            {
                if ((request.StartsAt != requestDTO.StartsAt) ||
                    (request.EndsOn != requestDTO.EndsOn) ||
                    (request.Note != requestDTO.Note))
                {
                    var deniedApproval = request.Approvals.FirstOrDefault(a =>
                    a.Status.Id == (int)TimeOffRequestApprovalStatusesEnum.Denied);

                    deniedApproval.Status = unitOfWork.RequestApprovalStatuses.Get(
                    (int)TimeOffRequestApprovalStatusesEnum.Requested);

                    await SendNotificationAsync(deniedApproval, request, " changed the request that was denied by you");
                }

                request.StartsAt = requestDTO.StartsAt;
                request.EndsOn = requestDTO.EndsOn;
                request.Note = requestDTO.Note;

                if (request.TypeId != (int)requestDTO.TypeId)
                {
                    request.TypeId = (int)requestDTO.TypeId;
                    request.Policy = GetEmployeePositionTimeOffPolicyByTypeAndPosition(
                        (int)requestDTO.TypeId, user.PositionId);
                }

                var currentUsersApproveRequestId = request.Approvals.Select(i => i.UserId);
                var newUsersApproveRequestId = requestDTO.ApproversId;

                bool isEqual = (currentUsersApproveRequestId.Count() == newUsersApproveRequestId.Count()
                    && (!currentUsersApproveRequestId.Except(newUsersApproveRequestId).Any()
                    || !newUsersApproveRequestId.Except(currentUsersApproveRequestId).Any()));//equality test of current and new approvers


                if (!isEqual)
                {
                    foreach (var approval in request.Approvals)
                    {
                        unitOfWork.RequestApprovals.Delete(approval.Id);
                    }

                    CreateTimeOffRequestApprovalsForRequest(request, requestDTO.ApproversId);

                }
            }

            await unitOfWork.SaveAsync();
        }

        public TimeOffRequestDTO GetById(int requestId)
        {
            var request = unitOfWork.TimeOffRequests.Get(requestId);

            if (request == null)
            {
                throw new EntityNotFoundException<TimeOffRequest>(requestId);
            }

            return mapper.Map<TimeOffRequest, TimeOffRequestDTO>(request);
        }

        public TimeOffRequestDTO GetById(int requestId, bool loadRequestApprovers)
        {
            var request = GetById(requestId);

            if (loadRequestApprovers)
            {
                var currentApproversId = request.Approvals.Select(i => i.UserId).ToList(); ;
                request.ApproversId = currentApproversId;
            }

            return request;
        }

        public IEnumerable<TimeOffRequestDTO> GetAll()
        {
            var requests = unitOfWork.TimeOffRequests.GetAll();
            var requestsDTO = mapper.Map<IEnumerable<TimeOffRequest>, IEnumerable<TimeOffRequestDTO>>(requests);

            return requestsDTO;
        }

        public IEnumerable<TimeOffRequestDTO> GetAllForCurrentUser(string userid)
        {
            var requests = unitOfWork.TimeOffRequests.Filter(r => r.UserId == userid);
            var requestsDTO = mapper.Map<IEnumerable<TimeOffRequest>, IEnumerable<TimeOffRequestDTO>>(requests);

            return requestsDTO;
        }

        public async Task<IEnumerable<IEnumerable<User>>> GetUsersAsync(int typeId, int positionId, UserManager<User> userManager)
        {
            var appropriateEmployeePositionTimeOffPolicy = GetEmployeePositionTimeOffPolicyByTypeAndPosition(typeId, positionId);

            if (appropriateEmployeePositionTimeOffPolicy == null)
            {
                throw new EntityNotFoundException("Appropriate policy");
            }

            var approvers = appropriateEmployeePositionTimeOffPolicy.Approvers;

            List<List<User>> listsOfAviableUsersAsApproversByPosition = new List<List<User>>();

            foreach (var approver in approvers)
            {
                for (int i = 0; i < approver.Amount; i++)
                {
                    listsOfAviableUsersAsApproversByPosition.Add(
                        userManager.Users.Where(u =>
                        u.PositionId == approver.EmployeePositionId).ToList());
                }
            }

            if (listsOfAviableUsersAsApproversByPosition == null
                || !listsOfAviableUsersAsApproversByPosition.Any())
            {
                throw new ApprovalsNotFoundException("Approvals for appropriate policy");
            }

            List<List<User>> listsOfAviableUsersAsApproversInRoleApprover = new List<List<User>>();

            foreach (var usersList in listsOfAviableUsersAsApproversByPosition)
            {
                List<User> ListApproversInRoleApprover = new List<User>();

                foreach (var user in usersList)
                {
                    IList<string> userRoles = await _userManager.GetRolesAsync(user);
                    var foundRole = userRoles.FirstOrDefault(role => role == "Approver");

                    if (foundRole != null)
                    {
                        ListApproversInRoleApprover.Add(user);
                    }
                }

                listsOfAviableUsersAsApproversInRoleApprover.Add(ListApproversInRoleApprover);

            }

            return listsOfAviableUsersAsApproversInRoleApprover;
        }

        public EmployeePositionTimeOffPolicy GetEmployeePositionTimeOffPolicyByTypeAndPosition
            (int typeId, int positionId)
        {
            var policy = unitOfWork.EmployeePositionTimeOffPolicies.Find(
              emtp => emtp.TypeId == typeId &&
              emtp.PositionId == positionId &&
              emtp.IsActive == true);
            if (policy != null)
            {
                return policy;
            }
            else
            {
                policy = unitOfWork.EmployeePositionTimeOffPolicies.Find(
                    emtp => emtp.TypeId == typeId &&
                    emtp.PositionId == null &&
                    emtp.IsActive == true);
                return policy;
            }
        }

        public void CreateTimeOffRequestApprovalsForRequest
           (TimeOffRequest request, IEnumerable<string> userPolicyApproversId)
        {

            var approvals = new List<TimeOffRequestApproval>();

            foreach (var approverId in userPolicyApproversId)
            {
                var approval = new TimeOffRequestApproval
                {
                    UserId = approverId,
                    TimeOffRequest = request,
                    Status = unitOfWork.RequestApprovalStatuses.Get(
                    (int)TimeOffRequestApprovalStatusesEnum.InProgres)
                };

                approvals.Add(approval);
            }

            approvals.FirstOrDefault().Status = unitOfWork.RequestApprovalStatuses.Get(
                    (int)TimeOffRequestApprovalStatusesEnum.Requested);

            foreach (var approval in approvals)
            {
                unitOfWork.RequestApprovals.Create(approval);
            }
        }

        public bool IfApprovedAtLeastOnce(int id)
        {
            var request = unitOfWork.TimeOffRequests.Get(id);

            if (request == null)
            {
                throw new EntityNotFoundException<TimeOffRequest>();
            }

            return request.Approvals.Any(a => a.Status.Id == (int)TimeOffRequestApprovalStatusesEnum.Accepted);
        }

        async Task SendNotificationAsync(TimeOffRequestApproval approval, TimeOffRequest request, string descriptionOfReason)
        {
            if (approval == null)
            {
                return;
            }

            var addressee = await _userManager.FindByIdAsync(approval.UserId);
            var requesting = await _userManager.FindByIdAsync(request.UserId);

            string mailAddressee = addressee.Email;
            string mailRequesting = requesting.Email;

            string description = $"User {mailRequesting} {descriptionOfReason}";

            SendMail(mailAddressee, description);
        }

        void SendMail(string mailAddressee, string description)
        {
            MailAddress sender = new MailAddress("totapriorit2018@gmail.com", "Time Off Tracker");

            MailAddress addressee = new MailAddress(mailAddressee);

            MailMessage message = new MailMessage(sender, addressee);

            message.Subject = "There are requests that need your approvement.";
            message.Body = "<p>" + description + " </br>" +
                "  <a href=\"https://tot-apriorit.azurewebsites.net\"> View it on Time Off Tracker</a>.</p>";
            message.IsBodyHtml = true;

            //In the case of access arror check that "Unsafe applications allowed" in account 
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("totapriorit2018@gmail.com", "Password-TOT1");
            smtp.EnableSsl = true;

            //smtp.Send(message);
        }

    }
}
