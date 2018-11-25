using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TOT.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using TOT.Entities.TimeOffRequests;

namespace TOT.Data.Repositories
{
    class TimeOffRequestApprovalRepository : IRepository<TimeOffRequestApproval>
    {
        private readonly DbSet<TimeOffRequestApproval> set;

        public TimeOffRequestApprovalRepository(TOTDBContext context)
        {
            set = context.Set<TimeOffRequestApproval>();          
        }

        public void Create(TimeOffRequestApproval item)
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

        public IEnumerable<TimeOffRequestApproval> Filter(Expression<Func<TimeOffRequestApproval, bool>> predicate)
        {
            return set.Where(predicate)
                .Include(t => t.User)
                .Include(t => t.Status)
                .Include(t => t.TimeOffRequest)
                .ThenInclude(t => t.Type)
                .Include(t => t.TimeOffRequest)
                .ThenInclude(t => t.User);
        }

        public TimeOffRequestApproval Find(Expression<Func<TimeOffRequestApproval, bool>> predicate)
        {
            return set
                .Include(t => t.User)
                .Include(t => t.Status)
                .Include(t => t.TimeOffRequest)
                .ThenInclude(t => t.Type)
                .Include(t => t.TimeOffRequest)
                .ThenInclude(t => t.User)
                .FirstOrDefault(predicate);
        }

        public TimeOffRequestApproval Get(int id)
        {
            return set
                .Include(t => t.User)
                .Include(t => t.Status)
                .Include(t => t.TimeOffRequest)
                .ThenInclude(t => t.Type)
                .Include(t => t.TimeOffRequest)
                .ThenInclude(t => t.User)
                .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TimeOffRequestApproval> GetAll()
        {
            return set
                .Include(t => t.User)
                .Include(t => t.Status)
                .Include(t => t.TimeOffRequest)
                .ThenInclude(t => t.Type)
                .Include(t => t.TimeOffRequest)
                .ThenInclude(t => t.User);
        }

        public void Update(TimeOffRequestApproval item)
        {
            set.Update(item);
        }
    }
}
