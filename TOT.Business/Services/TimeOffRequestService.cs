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

namespace TOT.Business.Services
{
    public class TimeOffRequestService : BaseService
    {
        public TimeOffRequestService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public Task CreateAsync(TimeOffRequestDTO requestDTO, User user)
        {
            if (requestDTO == null)
            {
                throw new ArgumentNullException(nameof(requestDTO));
            }

            var entry = mapper.Map<TimeOffRequestDTO, TimeOffRequest>(requestDTO);

            entry.User = user.Id;
            entry.Policy = GetEmployeePositionTimeOffPolicyByTypeAndPosition((int)requestDTO.TypeId, user.PositionId);
            unitOfWork.TimeOffRequests.Create(entry);
            CreateTimeOffRequestApprovalsForRequest(entry, requestDTO.ApproversId);

            return unitOfWork.SaveAsync();
        }

        public Task DeleteAsync(int id)
        {
            unitOfWork.TimeOffRequests.Delete(id);

            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TimeOffRequestDTO requestDTO, User user)
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

            return unitOfWork.SaveAsync();
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
            var requests = unitOfWork.TimeOffRequests.Filter(r => r.User == userid);
            var requestsDTO = mapper.Map<IEnumerable<TimeOffRequest>, IEnumerable<TimeOffRequestDTO>>(requests);

            return requestsDTO;
        }

        public IEnumerable<User> GetUsers(int typeId, int positionId, UserManager<User> userManager)
        {

            var appropriateEmployeePositionTimeOffPolicy = GetEmployeePositionTimeOffPolicyByTypeAndPosition(typeId, positionId);

            if (appropriateEmployeePositionTimeOffPolicy == null)
            {
                throw new EntityNotFoundException("Appropriate policy");
            }

            var approvers = appropriateEmployeePositionTimeOffPolicy.Approvals;
            var aviableUserAsApprovers = userManager.Users.Where(u =>
            approvers.Any(a => a.UserId == u.Id));//select Users available to approve requests this policy

            if (aviableUserAsApprovers == null || !aviableUserAsApprovers.Any())
            {
                throw new ApprovalsNotFoundException("Approvals for appropriate policy");
            }

            return aviableUserAsApprovers;
        }

        public EmployeePositionTimeOffPolicy GetEmployeePositionTimeOffPolicyByTypeAndPosition
            (int typeId, int positionId)
        {
            return unitOfWork.EmployeePositionTimeOffPolicy.Find(
              emtp => emtp.TypeId == typeId && emtp.PositionId == positionId);
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

    }
}
