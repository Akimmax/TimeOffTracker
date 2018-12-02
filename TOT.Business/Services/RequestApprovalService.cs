using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TOT.Business.Exceptions;
using TOT.Entities.TimeOffRequests;
using TOT.Interfaces;
using TOT.Dto.TimeOffRequests;
using System.Linq;
using System.Net.Mail;
using System.Net;

namespace TOT.Business.Services
{
    public class RequestApprovalService : BaseService
    {
        public RequestApprovalService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public IEnumerable<TimeOffRequestApprovalDTO> GetAll()
        {
            var approvals = unitOfWork.RequestApprovals.GetAll().OrderBy(a => a.Id).OrderBy(a => a.Id);
            var approvalsDTO = mapper.Map<IEnumerable<TimeOffRequestApproval>, IEnumerable<TimeOffRequestApprovalDTO>>(approvals);

            return approvalsDTO;
        }

        public IEnumerable<TimeOffRequestApprovalDTO> GetAllForCurrentUser(string userid)
        {
            var approvals = unitOfWork.RequestApprovals.Filter(a => a.UserId == userid).OrderBy(a => a.Id);
            var approvalsDTO = mapper.Map<IEnumerable<TimeOffRequestApproval>, IEnumerable<TimeOffRequestApprovalDTO>>(approvals);

            return approvalsDTO;
        }

        public IEnumerable<TimeOffRequestApprovalDTO> GetRefusedForCurrentUser(string userid)
        {
            var approvals = unitOfWork.RequestApprovals.Filter(a =>
            a.Status.Id == (int)TimeOffRequestApprovalStatusesEnum.Denied && a.UserId == userid);
            var approvalsDTO = mapper.Map<IEnumerable<TimeOffRequestApproval>, IEnumerable<TimeOffRequestApprovalDTO>>(approvals);

            return approvalsDTO;
        }

        public IEnumerable<TimeOffRequestApprovalDTO> GetRequestedForCurrentUser(string userid)
        {
            var approvals = unitOfWork.RequestApprovals.Filter(a =>
            a.Status.Id == (int)TimeOffRequestApprovalStatusesEnum.Requested && a.UserId == userid).OrderBy(a => a.Id);
            var approvalsDTO = mapper.Map<IEnumerable<TimeOffRequestApproval>, IEnumerable<TimeOffRequestApprovalDTO>>(approvals);

            return approvalsDTO;
        }

        public TimeOffRequestApprovalDTO GetById(int approvalId)
        {
            var approval = unitOfWork.RequestApprovals.Get(approvalId);

            if (approval == null)
            {
                throw new EntityNotFoundException<TimeOffRequestApproval>(approvalId);
            }

            return mapper.Map<TimeOffRequestApproval, TimeOffRequestApprovalDTO>(approval);
        }

        public TimeOffRequestDTO GetRequestForApproval(int approvalId)
        {
            var approval = unitOfWork.RequestApprovals.Get(approvalId);

            if (approval == null)
            {
                throw new EntityNotFoundException<TimeOffRequest>(approvalId);
            }

            var request = unitOfWork.TimeOffRequests.Get(approval.TimeOffRequest.Id);

            request.Approvals=request.Approvals.OrderBy(a => a.Id).ToList();

            if (request == null)
            {
                throw new EntityNotFoundException<TimeOffRequest>(approval.TimeOffRequest.Id);
            }

            return mapper.Map<TimeOffRequest, TimeOffRequestDTO>(request);
        }

        public TimeOffRequestApproval SetNextAsRequested
          (TimeOffRequestApproval accepted)
        {
            var approval = unitOfWork.RequestApprovals.Find(
                a => a.TimeOffRequest.Id == accepted.TimeOffRequestId
                && a.Status.Id == (int)TimeOffRequestApprovalStatusesEnum.Queued);

            if (approval != null)
            {
                approval.Status = unitOfWork.RequestApprovalStatuses.Get(
                (int)TimeOffRequestApprovalStatusesEnum.Requested);
            }
            return approval;
        }

        public Task Refuse(int id, string reason, string userid)
        {
            var approval = unitOfWork.RequestApprovals.Get(id);

            if (approval == null)
            {
                throw new EntityNotFoundException<TimeOffRequestApproval>(id);
            }

            if (approval.UserId != userid)
            {
                throw new UnauthorizedAccessException();
            }

            approval.Status = unitOfWork.RequestApprovalStatuses.Get(
                 (int)TimeOffRequestApprovalStatusesEnum.Denied);
            approval.SolvedDate = DateTime.Now;

            if (approval.Reason != reason && reason!=null)
            {
                approval.Reason = reason;
            }

            return unitOfWork.SaveAsync();
        }

        public Task Approve(int id, string reason, string userid)
        {
            var approval = unitOfWork.RequestApprovals.Get(id);

            if (approval == null)
            {
                throw new EntityNotFoundException<TimeOffRequestApproval>(id);
            }

            if (approval.UserId != userid)
            {
                throw new UnauthorizedAccessException();
            }

            approval.Status = unitOfWork.RequestApprovalStatuses.Get(
            (int)TimeOffRequestApprovalStatusesEnum.Accepted);
            approval.SolvedDate = DateTime.Now;

            if (approval.Reason != reason && reason != null)
            {
                approval.Reason = reason;
            }

            var nextapproval = SetNextAsRequested(approval);
            SendNotification(nextapproval);

            return unitOfWork.SaveAsync();
        }

        public Task UpdateComment(int id, string reason, string userid)
        {
            var approval = unitOfWork.RequestApprovals.Get(id);

            if (approval == null)
            {
                throw new EntityNotFoundException<TimeOffRequestApproval>(id);
            }

            if (approval.UserId != userid)
            {
                throw new UnauthorizedAccessException();
            }

            if (approval.Reason != reason && reason != null)
            {
                approval.Reason = reason;
            }
            
            approval.SolvedDate = DateTime.Now;

            return unitOfWork.SaveAsync();
        }

        void SendNotification(TimeOffRequestApproval approval)
        {
            if (approval == null)
            {
                return;
            }

            var request = unitOfWork.TimeOffRequests.Get(approval.TimeOffRequest.Id);
            string mailAddressee = approval.User.Email;
            string mailRequesting = request.User.Email;
           
            //SendMail(mailAddressee, mailRequesting);
        }

        void SendMail(string mailAddressee, string usernameRequesting)
        {
            MailAddress sender = new MailAddress("totapriorit2018@gmail.com", "Time Off Tracker");

            MailAddress addressee = new MailAddress(mailAddressee);

            MailMessage message = new MailMessage(sender, addressee);

            message.Subject = "There are requests that need your approvement.";
            message.Body = "<p> User " + usernameRequesting + " requested your to approve request </br>" +
                "  <a href=\"https://tot-apriorit.azurewebsites.net\"> View it on Time Off Tracker</a>.</p>";
            message.IsBodyHtml = true;

            //In the case of access arror check that "Unsafe applications allowed" in account 
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); 
            smtp.Credentials = new NetworkCredential("totapriorit2018@gmail.com", "Password-TOT1");
            smtp.EnableSsl = true;

            smtp.Send(message);
        }
    }
}
