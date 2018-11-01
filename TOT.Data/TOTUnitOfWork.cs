using System.Threading.Tasks;
using TOT.Data.Repositories;
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
            RequestApprovals = new BasicRepository<TimeOffRequestApproval>(context);
            RequestApprovalStatuses = new BasicRepository<TimeOffRequestApprovalStatuses>(context);
            TimeOffRequests = new TimeOffRequestRepository(context);
            TimeOffTypes = new BasicRepository<TimeOffType>(context);
        }

        public IRepository<TimeOffRequestApproval> RequestApprovals { get; }
        public IRepository<TimeOffRequestApprovalStatuses> RequestApprovalStatuses { get; }
        public IRepository<TimeOffRequest> TimeOffRequests { get; }
        public IRepository<TimeOffType> TimeOffTypes { get; }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
