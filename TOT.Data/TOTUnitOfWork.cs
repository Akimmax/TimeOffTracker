using System.Threading.Tasks;
using TOT.Data.Repositories;
using TOT.Entities;
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
            EmployeePositions = new BasicRepository<EmployeePosition>(context);
            RequestApprovals = new TimeOffRequestApprovalRepository(context);
            RequestApprovalStatuses = new BasicRepository<TimeOffRequestApprovalStatuses>(context);

            TimeOffPolicies = new TimeOffPolicyRepository(context);
            TimeOffPolicyApprovers = new TimeOffPolicyApproversRepository(context);
            EmployeePositionTimeOffPolicies = new EmployeePositionTimeOffPolicyRepository(context);
        }

        public IRepository<TimeOffType> TimeOffTypes { get; }
        public IRepository<TimeOffRequest> TimeOffRequests { get; }
        public IRepository<EmployeePosition> EmployeePositions { get; }
        public IRepository<TimeOffRequestApproval> RequestApprovals { get; }
        public IRepository<TimeOffRequestApprovalStatuses> RequestApprovalStatuses { get; }

        public IRepository<TimeOffPolicy> TimeOffPolicies { get; }
        public IRepository<TimeOffPolicyApprover> TimeOffPolicyApprovers { get; }
        public IRepository<EmployeePositionTimeOffPolicy> EmployeePositionTimeOffPolicies { get; }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
