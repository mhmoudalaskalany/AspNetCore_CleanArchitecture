using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BackendCore.Common.Abstraction.Repository;
using BackendCore.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BackendCore.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected DbSet<T> DbSet;
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }
        public async Task<T> GetAsync(params object[] keys)
        {
            return await DbSet.FindAsync(keys);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderByCriteria != null)
            {
                query = query.OrderBy(orderByCriteria);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }
        public async Task<(int, IEnumerable<T>)> FindPaggedAsync(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            int count = query.Count();
            if (orderByCriteria != null)
            {
                query = query.OrderBy(orderByCriteria).Skip(skip).Take(take);
            }
            if (include != null)
            {
                query = include(query);
            }
            return (count, await query.ToListAsync());
        }
        public async Task<IEnumerable<T>> GetAllAsync(IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (include != null)
            {
                query = include(query);
            }
            if (orderByCriteria != null)
            {
                query = query.OrderBy(orderByCriteria);
            }
            return await query.ToListAsync();
        }

        public T Add(T newEntity)
        {
            return DbSet.Add(newEntity).Entity;
        }
        public void AddRange(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }
        public void Update(T originalEntity, T newEntity)
        {
            Context.Entry(originalEntity).CurrentValues.SetValues(newEntity);
        }
        public void UpdateRange(IEnumerable<T> newEntitie)
        {
            Context.UpdateRange(newEntitie);
        }
        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
        public async void Remove(Expression<Func<T, bool>> predicate)
        {
            var objects = await DbSet.FindAsync(predicate);
            DbSet.RemoveRange(objects);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}
