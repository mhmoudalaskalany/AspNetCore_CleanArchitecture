using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Template.Common.Extensions;
using Template.Common.Infrastructure.Repository;

namespace Template.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext DbContext;
        protected DbSet<T> DbSet;


        public Repository(DbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<T>();
        }

        public async Task<T> GetAsync(params object[] keys)
        {
            return await DbSet.FindAsync(keys);
        }

        public IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return DbSet.Where(predicate);
            }
            return DbSet;
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
                query = include(query).AsSplitQuery();
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
                query = include(query).AsSplitQuery();
            }
            return await query.ToListAsync();
        }
        
        public async Task<(int, IEnumerable<T>)> FindPagedAsync(Expression<Func<T, bool>> predicate = null, int pageNumber = 0, int pageSize = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
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
                var field = orderByCriteria.First().PairAsSqlExpression;
                query = query.OrderBy(field);
            }
            if (include != null)
            {
                query = include(query).AsSplitQuery();
            }

            var count = query.Count();

            return (count, await query.Skip(pageNumber).Take(pageSize).ToListAsync());
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
                query = include(query).AsSplitQuery();
            }
            if (orderByCriteria != null)
            {
                query = query.OrderBy(orderByCriteria);
            }
            return await query.ToListAsync();
        }
     

        public async Task<ICollection<TType>> GetSelectAsync<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select) where TType : class
        {
            return await DbSet.Where(where).Select(select).ToListAsync();
        }
     
        public async Task<IEnumerable<TType>> FindSelectAsync<TType>(Expression<Func<T, TType>> select, Expression<Func<T, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true) where TType : class
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
                query = include(query).AsSplitQuery();
            }
            return await query.Select(select).ToListAsync();
        }
        
        public async Task<(int, IEnumerable<TType>)> FindPagedSelectAsync<TType>(Expression<Func<T, TType>> select, Expression<Func<T, bool>> predicate = null, int pageNumber = 0, int pageSize = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true) where TType : class
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
            if (include != null)
            {
                query = include(query).AsSplitQuery();
            }
            var count = query.Count();

            if (orderByCriteria == null) return (count, await query.Skip(pageNumber).Take(pageSize).Select(select).ToListAsync());

            var field = orderByCriteria.First().PairAsSqlExpression;

            query = query.OrderBy(field);

            return (count, await query.Skip(pageNumber).Take(pageSize).Select(select).ToListAsync());
        }
      
        public IList<TReturn> FindGrouped<TResult, TKey, TGroup, TReturn>(
            List<Expression<Func<T, bool>>> predicates,
            Expression<Func<T, TResult>> firstSelector,
            Expression<Func<TResult, TKey>> orderSelector,
            Func<TResult, TGroup> groupSelector,
            Func<IGrouping<TGroup, TResult>, TReturn> selector, bool isDesc = false,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int skip = 0, int take = int.MaxValue)
        {
            IQueryable<T> query = DbSet;
            if (include != null)
            {
                query = include(query).AsSplitQuery();
            }

            var result = predicates
                .Aggregate(query, (current, predicate) => current.Where(predicate))
                .Select(firstSelector);
            return isDesc ? result.OrderByDescending(orderSelector).GroupBy(groupSelector).Select(selector).Skip(skip).Take(take).ToList() : result.OrderBy(orderSelector).GroupBy(groupSelector).Select(selector).Skip(skip).Take(take).ToList();
        }
        
        public IEnumerable<TB> ExecuteStored<TB>(string sql) where TB : class
        {
            var result = DbContext.Set<TB>().FromSqlRaw(sql);
            return result;
        }
        
        public async Task<int> ExecWithStoreProcedure(string query)
        {
            return await DbContext.Database.ExecuteSqlRawAsync(query);
        }
        
        public long GetNextSequenceValue(string sequenceName)
        {
            var value = DbContext.GetNextSequenceValue(sequenceName);
            return value;
        }

       
        public async Task<int> Count(Expression<Func<T, bool>> predicate = null) => predicate == null ? await DbSet.CountAsync() : await DbSet.CountAsync(predicate);
        
        public async Task<TB> Max<TB>(Expression<Func<T, TB>> selector, Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await DbSet.MaxAsync(selector);
            return await DbSet.Where(predicate).MaxAsync(selector);
        }
       
        public async Task<bool> Any(Expression<Func<T, bool>> predicate = null) => predicate == null ? await DbSet.AnyAsync() : await DbSet.AnyAsync(predicate);

        public IQueryable<T> Union(params IQueryable<T>[] queries)
        {
            if (queries == null || queries.Length == 0)
            {
                throw new ArgumentException("At least one queryable must be provided", nameof(queries));
            }

            IQueryable<T> result = queries[0];
            for (int i = 1; i < queries.Length; i++)
            {
                result = result.Union(queries[i]);
            }

            return result;
        }

        public T Add(T newEntity)
        {
            return DbSet.Add(newEntity).Entity;
        }
      
        public async Task<T> AddAsync(T newEntity)
        {
            var result = await DbSet.AddAsync(newEntity);
            return result.Entity;
        }
  
        public void AddRange(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public void Update(T originalEntity, T newEntity)
        {
            DbContext.Entry(originalEntity).CurrentValues.SetValues(newEntity);
        }
       
        public async Task UpdateAsync(object id, T newEntity)
        {
            var originalEntity = await DbSet.FindAsync(id);
            if (originalEntity != null)
            {
                DbContext.Entry(originalEntity).CurrentValues.SetValues(newEntity);
            }
        }
        
        public void UpdateRange(IEnumerable<T> newEntities)
        {
            DbContext.UpdateRange(newEntities);
        }
        
        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
        
        public void RemoveLogical(T entity)
        {
            var type = entity.GetType();
            var property = type.GetProperty("IsDeleted");
            if (property != null) property.SetValue(entity, true);
            var id = type.GetProperty("Id")?.GetValue(entity);
            var original = DbSet.Find(id);
            Update(original, entity);
        }
       
        public async void Remove(Expression<Func<T, bool>> predicate)
        {
            var objects = await DbSet.FindAsync(predicate);
            if (objects != null)
            {
                DbSet.RemoveRange(objects);
            }
        }
       
        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

         public async Task<int> RemoveBulkAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.Where(predicate).ExecuteDeleteAsync();
        }

    }
}
