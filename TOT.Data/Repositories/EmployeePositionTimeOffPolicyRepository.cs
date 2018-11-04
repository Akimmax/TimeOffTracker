using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TOT.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using TOT.Entities.TimeOffPolicies;

namespace TOT.Data.Repositories
{
    class EmployeePositionTimeOffPolicyRepository : IRepository<EmployeePositionTimeOffPolicy>
    {
        private readonly DbSet<EmployeePositionTimeOffPolicy> set;

        public EmployeePositionTimeOffPolicyRepository(TOTDBContext context)
        {
            set = context.Set<EmployeePositionTimeOffPolicy>();
        }

        public void Create(EmployeePositionTimeOffPolicy item)
        {
            set.Add(item);
        }

        public void Delete(int id)
        {
            var item = set.Find(id);
            if (item != null)
            {
                set.Remove(item);
            }
        }

        public IEnumerable<EmployeePositionTimeOffPolicy> Filter(Expression<Func<EmployeePositionTimeOffPolicy, bool>> predicate)
        {
            return set.Where(predicate);
        }

        public EmployeePositionTimeOffPolicy Find(Expression<Func<EmployeePositionTimeOffPolicy, bool>> predicate)
        {
            return set
                .Include(t => t.Approvals)
                .FirstOrDefault(predicate);
        }

        public EmployeePositionTimeOffPolicy Get(int id)
        {
            return set.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<EmployeePositionTimeOffPolicy> GetAll()
        {
            return set;
        }

        public void Update(EmployeePositionTimeOffPolicy item)
        {
            set.Update(item);
        }
    }
}

