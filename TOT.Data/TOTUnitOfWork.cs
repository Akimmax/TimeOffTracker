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
        }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
