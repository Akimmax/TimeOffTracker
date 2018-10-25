using System;
using System.Threading.Tasks;
using TOT.Entities.TimeOffRequests;

namespace TOT.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TimeOffRequestApproval> RequestApprovals { get; }
        IRepository<TimeOffRequestApprovalStatuses> RequestApprovalStatuses { get; }
        IRepository<TimeOffRequest> TimeOffRequests { get; }
        IRepository<TimeOffType> TimeOffTypes { get; }

        Task SaveAsync();
    }
}
