using System.Threading.Tasks;
using TOT.Data.Repositories;
using TOT.Entities.TimeOffPolicies;
using TOT.Entities.TimeOffRequests;
using TOT.Interfaces;

namespace TOT.Data
{
    public class TOTUnitOfWork : IUnitOfWork
    {
        private readonly TOTDBContext dbContext;

        public TOTUnitOfWork(TOTDBContext context)
        {
            dbContext = context;
            TimeOffRequests = new TimeOffRequestRepository(context);
            TimeOffTypes = new BasicRepository<TimeOffType>(context);
            RequestApprovals = new BasicRepository<TimeOffRequestApproval>(context);
            RequestApprovalStatuses = new BasicRepository<TimeOffRequestApprovalStatuses>(context);

            TimeOffPolicies = new TimeOffPolicyRepository(context);
            TimeOffPolicyApprovals = new TimeOffPolicyApprovalsRepository(context);
            EmployeePositionTimeOffPolicies = new EmployeePositionTimeOffPolicyRepository(context);
        }

        public IRepository<TimeOffType> TimeOffTypes { get; }
        public IRepository<TimeOffRequest> TimeOffRequests { get; }
        public IRepository<TimeOffRequestApproval> RequestApprovals { get; }
        public IRepository<TimeOffRequestApprovalStatuses> RequestApprovalStatuses { get; }

        public IRepository<TimeOffPolicy> TimeOffPolicies { get; }
        public IRepository<TimeOffPolicyApproval> TimeOffPolicyApprovals { get; }
        public IRepository<EmployeePositionTimeOffPolicy> EmployeePositionTimeOffPolicies { get; }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
