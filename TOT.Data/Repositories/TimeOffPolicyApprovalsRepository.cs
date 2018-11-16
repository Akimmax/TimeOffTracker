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
    class TimeOffPolicyApprovalsRepository : IRepository<TimeOffPolicyApproval>
    {
        private readonly TOTDBContext dbContext;

        public TimeOffPolicyApprovalsRepository(TOTDBContext context)
        {
            dbContext = context;
        }

        public TimeOffPolicyApproval Get(int id)
        {
            return GetAllQuery().FirstOrDefault(x => x.Id == id);
        }

        public void Create(TimeOffPolicyApproval item)
        {
            dbContext.Set<TimeOffPolicyApproval>().Add(item);
        }

        public void Update(TimeOffPolicyApproval item)
        {
            dbContext.Set<TimeOffPolicyApproval>().Update(item);
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<TimeOffPolicyApproval>().Find(id);
            if (item != null)
            {
                dbContext.Set<TimeOffPolicyApproval>().Remove(item);
            }
        }

        public IEnumerable<TimeOffPolicyApproval> Filter(Expression<Func<TimeOffPolicyApproval, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .ToList();
        }

        public TimeOffPolicyApproval Find(Expression<Func<TimeOffPolicyApproval, bool>> predicate)
        {
            return GetAllQuery()
                .FirstOrDefault(predicate);
        }

        public IEnumerable<TimeOffPolicyApproval> GetAll()
        {
            return GetAllQuery().ToList();
        }

        protected IQueryable<TimeOffPolicyApproval> GetAllQuery()
        {
            return dbContext.Set<TimeOffPolicyApproval>()
                .Include(x=>x.EmployeePosition);
        }
    }
}
