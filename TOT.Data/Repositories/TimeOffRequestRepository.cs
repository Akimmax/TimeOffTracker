using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TOT.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using TOT.Entities.TimeOffRequests;

namespace TOT.Data.Repositories
{
    class TimeOffRequestRepository : IRepository<TimeOffRequest>
    {
        private readonly DbSet<TimeOffRequest> set;

        public TimeOffRequestRepository(TOTDBContext context)
        {
            set = context.Set<TimeOffRequest>();
        }

        public void Create(TimeOffRequest item)
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

        public IEnumerable<TimeOffRequest> Filter(Expression<Func<TimeOffRequest, bool>> predicate)
        {
            return set.Where(predicate)
               .Include(t => t.Type)
               .Include(t => t.Approvals)
               .ThenInclude(t => t.Status);
        }

        public TimeOffRequest Find(Expression<Func<TimeOffRequest, bool>> predicate)
        {
            return set
                .Include(t => t.Type)
                .Include(t => t.Approvals)
                .ThenInclude(t => t.Status)
                .FirstOrDefault(predicate);
        }

        public TimeOffRequest Get(int id)
        {
            return set
                .Include(t => t.Type)
                .Include(t => t.Approvals)
                .ThenInclude(t => t.Status)
                .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TimeOffRequest> GetAll()
        {
            return set
                .Include(t => t.Type)
                .Include(t => t.Approvals)
                .ThenInclude(t => t.Status);
        }

        public void Update(TimeOffRequest item)
        {
            set.Update(item);
        }
    }
}
