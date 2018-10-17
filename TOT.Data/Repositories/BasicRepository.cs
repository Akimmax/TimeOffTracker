using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TOT.Interfaces;
using System.Linq.Expressions;
using System.Linq;

namespace TOT.Data.Repositories
{
    class BasicRepository<T> : IRepository<T>
        where T : class
    {
        private readonly DbSet<T> set;

        public BasicRepository(DBContext context)
        {
            set = context.Set<T>();
        }

        public void Create(T item)
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

        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return set.Where(predicate);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return set.FirstOrDefault(predicate);
        }

        public T Get(int id)
        {
            return set.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return set;
        }

        public void Update(T item)
        {
            set.Update(item);
        }
    }
}
