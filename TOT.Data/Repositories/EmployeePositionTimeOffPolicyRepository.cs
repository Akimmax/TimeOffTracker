using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using TOT.Entities.TimeOffPolicies;
using Microsoft.EntityFrameworkCore;
using TOT.Interfaces;

namespace TOT.Data.Repositories
{
    public class EmployeePositionTimeOffPolicyRepository : IRepository<EmployeePositionTimeOffPolicy>
    {
        private readonly TOTDBContext dbContext;

        public EmployeePositionTimeOffPolicyRepository(TOTDBContext context)
        {
            dbContext = context;
        }

        public EmployeePositionTimeOffPolicy Get(int id)
        {
            return GetAllQuery()
                .FirstOrDefault(x => x.Id == id);
        }

        public void Create(EmployeePositionTimeOffPolicy item)
        {
            dbContext.Set<EmployeePositionTimeOffPolicy>().Add(item);
        }

        public void Update(EmployeePositionTimeOffPolicy item)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                dbContext.Set<EmployeePositionTimeOffPolicy>().Update(item);

                var ApproversSet = dbContext.Set<TimeOffPolicyApprover>();
                var oldApprovers = ApproversSet.Where(x=>x.EmployeePositionTimeOffPolicyId == item.Id);
                var newApprovers = item.Approvals;

                var toAddAprovers = newApprovers.Except(oldApprovers);
                ApproversSet.AddRangeAsync(toAddAprovers);
                var toRemoveApprovers = oldApprovers.Except(newApprovers);
                ApproversSet.RemoveRange(toRemoveApprovers);

                transaction.Commit();
            }
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<EmployeePositionTimeOffPolicy>().Find(id);
            if (item != null)
            {
                dbContext.Set<EmployeePositionTimeOffPolicy>().Remove(item);
            }
        }

        public IEnumerable<EmployeePositionTimeOffPolicy> Filter(Expression<Func<EmployeePositionTimeOffPolicy, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .ToList();
        }

        public EmployeePositionTimeOffPolicy Find(Expression<Func<EmployeePositionTimeOffPolicy, bool>> predicate)
        {
            return GetAllQuery()
                .FirstOrDefault(predicate);
        }

        public IEnumerable<EmployeePositionTimeOffPolicy> GetAll()
        {
            return GetAllQuery()
                .ToList();
        }

        protected IQueryable<EmployeePositionTimeOffPolicy> GetAllQuery()
        {
            return dbContext.Set<EmployeePositionTimeOffPolicy>()
                .Include(x => x.Approvals)
                    .ThenInclude(p=>p.EmployeePosition)
                .Include(x => x.Policy)
                .Include(x => x.Position)
                .Include(x => x.Type);
        }
    }
}
