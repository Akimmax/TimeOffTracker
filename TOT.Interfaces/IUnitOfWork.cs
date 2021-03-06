using System.Threading.Tasks;
using TOT.Entities;
using TOT.Entities.TimeOffPolicies;
using TOT.Entities.TimeOffRequests;

namespace TOT.Interfaces
{
    public interface IUnitOfWork
    {

        IRepository<TimeOffType> TimeOffTypes { get; }
        IRepository<TimeOffRequest> TimeOffRequests { get; }
        IRepository<EmployeePosition> EmployeePositions { get; }
        IRepository<TimeOffRequestApproval> RequestApprovals { get; }
        IRepository<TimeOffRequestApprovalStatuses> RequestApprovalStatuses { get; }

        IRepository<TimeOffPolicy> TimeOffPolicies { get; }
        IRepository<TimeOffPolicyApprover> TimeOffPolicyApprovers { get;}
        IRepository<EmployeePositionTimeOffPolicy> EmployeePositionTimeOffPolicies { get; }

        Task SaveAsync();
    }
}
