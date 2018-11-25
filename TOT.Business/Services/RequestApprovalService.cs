using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TOT.Business.Exceptions;
using TOT.Entities.TimeOffRequests;
using TOT.Interfaces;
using TOT.Dto.TimeOffRequests;
using System.Linq;


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

        public TimeOffRequestDTO GetRequest(int approvalId)
        {
            var approval = unitOfWork.RequestApprovals.Get(approvalId);

            if (approval == null)
            {
                throw new EntityNotFoundException<TimeOffRequest>(approvalId);
            }

            var request = unitOfWork.TimeOffRequests.Get(approval.TimeOffRequest.Id);

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
                && a.Status.Id == (int)TimeOffRequestApprovalStatusesEnum.InProgres);

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
            approval.Reason = reason;
            approval.SolvedDate = DateTime.Now;

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
            approval.Reason = reason;
            approval.SolvedDate = DateTime.Now;

            var nextapproval = SetNextAsRequested(approval);           

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

            approval.Reason = reason;
            approval.SolvedDate = DateTime.Now;

            return unitOfWork.SaveAsync();
        }                        
    }
}
