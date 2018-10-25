using System.Threading.Tasks;
using TOT.Interfaces;

namespace TOT.Data
{
    public class TOTUnitOfWork : IUnitOfWork
    {
        private readonly TOTDBContext dbContext;

        public TOTUnitOfWork(TOTDBContext context)
        {
            dbContext = context;
            Checks = new BasicRepository<Check>(context);
            RequestStatuses = new BasicRepository<RequestStatus>(context);
            TimeOffRequests = new TimeOffRequestRepository(context);
            TimeOffTypes = new BasicRepository<TimeOffType>(context);
        }

        public IRepository<Check> Checks { get; }
        public IRepository<RequestStatus> RequestStatuses { get; }
        public IRepository<TimeOffRequest> TimeOffRequests { get; }
        public IRepository<TimeOffType> TimeOffTypes { get; }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
