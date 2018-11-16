using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using TOT.Entities.TimeOffPolicies;
using TOT.Interfaces;

namespace TOT.Data.Repositories
{
    class TimeOffPolicyRepository : IRepository<TimeOffPolicy>
    {
        private readonly TOTDBContext dbContext;

        public TimeOffPolicyRepository(TOTDBContext context)
        {
            dbContext = context;
        }

        public TimeOffPolicy Get(int id)
        {
            return GetAllQuery()
                .FirstOrDefault(x => x.Id == id);
        }

        public void Create(TimeOffPolicy item)
        {
            dbContext.Set<TimeOffPolicy>().Add(item);
        }

        public void Update(TimeOffPolicy item)
        {
            dbContext.Set<TimeOffPolicy>().Update(item);
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<TimeOffPolicy>().Find(id);
            if (item != null)
            {
                dbContext.Set<TimeOffPolicy>().Remove(item);
            }
        }

        public IEnumerable<TimeOffPolicy> Filter(Expression<Func<TimeOffPolicy, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .ToList();
        }

        public TimeOffPolicy Find(Expression<Func<TimeOffPolicy, bool>> predicate)
        {
            return GetAllQuery()
                .FirstOrDefault(predicate);
        }

        public IEnumerable<TimeOffPolicy> GetAll()
        {
            return GetAllQuery()
                .ToList();
        }

        protected IQueryable<TimeOffPolicy> GetAllQuery()
        {
            return dbContext.Set<TimeOffPolicy>();
        }
    }
}
