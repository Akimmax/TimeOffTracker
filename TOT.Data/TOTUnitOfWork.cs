using System.Threading.Tasks;
using TOT.Data.Repositories;
using TOT.Entities.Request_Entities;
using TOT.Interfaces;

namespace TOT.Data
{
    public class TOTUnitOfWork : IUnitOfWork
    {
        private readonly DBContext dbContext;

        public TOTUnitOfWork(DBContext context)
        {
            dbContext = context;
            TimeOffRequests = new BasicRepository<TimeOffRequest>(context);
            TimeOffTypes = new BasicRepository<TimeOffType>(context);
            Checks = new BasicRepository<Check>(context);
            RequstStatuses = new BasicRepository<RequestStatus>(context);
        }

        public IRepository<TimeOffRequest> TimeOffRequests { get; }
        public IRepository<TimeOffType> TimeOffTypes { get; }
        public IRepository<Check> Checks { get; }
        public IRepository<RequestStatus> RequstStatuses { get; }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
