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
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.Set<EmployeePositionTimeOffPolicy>().Add(item);
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public void Update(EmployeePositionTimeOffPolicy item)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var newItem = dbContext.Set<EmployeePositionTimeOffPolicy>().First(x => x.Id == item.Id);
                    newItem.Policy.Name = item.Policy.Name;
                    newItem.Policy.TimeOffDaysPerYear = item.Policy.TimeOffDaysPerYear;
                    newItem.Policy.DelayBeforeAvailable = item.Policy.DelayBeforeAvailable;
                    newItem.IsActive = item.IsActive;
                    newItem.PolicyId = item.Policy.Id;
                    newItem.TypeId = item.Type.Id;

                    var ApproversSet = dbContext.Set<TimeOffPolicyApprover>();
                    var oldApprovers = ApproversSet.Where(x => x.EmployeePositionTimeOffPolicyId == item.Id);
                    var newApprovers = item.Approvers;

                    var toAddAprovers = newApprovers.Except(oldApprovers);
                    ApproversSet.AddRangeAsync(toAddAprovers);
                    var toRemoveApprovers = oldApprovers.Except(newApprovers);
                    ApproversSet.RemoveRange(toRemoveApprovers);

                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public void Delete(int id)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var set = dbContext.Set<EmployeePositionTimeOffPolicy>();
                    var item = set.Find(id);
                    if (item != null)
                    {
                        set.Remove(item);
                    }
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
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
                .Include(x => x.Approvers)
                    .ThenInclude(p=>p.EmployeePosition)
                .Include(x => x.Policy)
                .Include(x => x.Position)
                .Include(x => x.Type);
        }
    }
}
