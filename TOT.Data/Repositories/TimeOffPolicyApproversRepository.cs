using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TOT.Entities.TimeOffPolicies;
using TOT.Interfaces;

namespace TOT.Data.Repositories
{
    class TimeOffPolicyApproversRepository : IRepository<TimeOffPolicyApprover>
    {
        private readonly TOTDBContext dbContext;

        public TimeOffPolicyApproversRepository(TOTDBContext context)
        {
            dbContext = context;
        }

        public TimeOffPolicyApprover Get(int id)
        {
            return GetAllQuery().FirstOrDefault(x => x.Id == id);
        }

        public void Create(TimeOffPolicyApprover item)
        {
            dbContext.Set<TimeOffPolicyApprover>().Add(item);
        }

        public void Update(TimeOffPolicyApprover item)
        {
            dbContext.Set<TimeOffPolicyApprover>().Update(item);
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<TimeOffPolicyApprover>().Find(id);
            if (item != null)
            {
                dbContext.Set<TimeOffPolicyApprover>().Remove(item);
            }
        }

        public IEnumerable<TimeOffPolicyApprover> Filter(Expression<Func<TimeOffPolicyApprover, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .ToList();
        }

        public TimeOffPolicyApprover Find(Expression<Func<TimeOffPolicyApprover, bool>> predicate)
        {
            return GetAllQuery()
                .FirstOrDefault(predicate);
        }

        public IEnumerable<TimeOffPolicyApprover> GetAll()
        {
            return GetAllQuery().ToList();
        }

        protected IQueryable<TimeOffPolicyApprover> GetAllQuery()
        {
            return dbContext.Set<TimeOffPolicyApprover>()
                .Include(x=>x.EmployeePosition);
        }
    }
}
