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
        }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
