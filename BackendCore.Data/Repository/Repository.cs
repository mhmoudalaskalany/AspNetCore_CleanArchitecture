using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using BackendCore.Common.Extensions;
using BackendCore.Common.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BackendCore.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Properties

        protected readonly DbContext Context;
        protected DbSet<T> DbSet;

        #endregion


        #region Constructors

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        #endregion


        #region Public Methods
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(params object[] keys)
        {
            return await DbSet.FindAsync(keys);
        }
        /// <summary>
        /// First Or Default
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderby"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Find
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderByCriteria"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Find Paged
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="orderByCriteria"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public async Task<(int, IEnumerable<T>)> FindPagedAsync(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
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
                var field = orderByCriteria.First().PairAsSqlExpression;
                query = query.OrderBy(field).Skip(skip).Take(take);
            }
            if (include != null)
            {
                query = include(query);
            }
            return (count, await query.ToListAsync());
        }
        /// <summary>
        /// Get All 
        /// </summary>
        /// <param name="orderByCriteria"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get Select
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="where"></param>
        /// <param name="select"></param>
        /// <returns></returns>

        public async Task<ICollection<TType>> GetSelectAsync<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select) where TType : class
        {
            return await DbSet.Where(where).Select(select).ToListAsync();
        }
        /// <summary>
        /// Find Select With Sort And Order
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="select"></param>
        /// <param name="predicate"></param>
        /// <param name="orderByCriteria"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
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
                query = include(query);
            }
            return await query.Select(select).ToListAsync();
        }
        /// <summary>
        /// Find Paged Select
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="select"></param>
        /// <param name="predicate"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="orderByCriteria"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public async Task<(int, IEnumerable<TType>)> FindPagedSelectAsync<TType>(Expression<Func<T, TType>> select, Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true) where TType : class
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
                query = include(query);
            }
            var count = query.Count();
            if (orderByCriteria == null) return (count, await query.Skip(skip).Take(take).Select(select).ToListAsync());
            var field = orderByCriteria.First().PairAsSqlExpression;
            query = query.OrderBy(field).Skip(skip).Take(take);

            return (count, await query.Select(select).ToListAsync());
        }
        /// <summary>
        /// Find Grouped With Sort And Order
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TGroup"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="predicates"></param>
        /// <param name="firstSelector"></param>
        /// <param name="orderSelector"></param>
        /// <param name="groupSelector"></param>
        /// <param name="selector"></param>
        /// <param name="isDesc"></param>
        /// <param name="include"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
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
                query = include(query);
            }

            var result = predicates
                .Aggregate(query, (current, predicate) => current.Where(predicate))
                .Select(firstSelector);
            return isDesc ? result.OrderByDescending(orderSelector).GroupBy(groupSelector).Select(selector).Skip(skip).Take(take).ToList() : result.OrderBy(orderSelector).GroupBy(groupSelector).Select(selector).Skip(skip).Take(take).ToList();
        }
        /// <summary>
        /// Execute Stored
        /// </summary>
        /// <typeparam name="TB"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<TB> ExecuteStored<TB>(string sql) where TB : class
        {
            var result = Context.Set<TB>().FromSqlRaw(sql);
            return result;
        }
        /// <summary>
        /// Exec Stored
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<int> ExecWithStoreProcedure(string query)
        {
            return await Context.Database.ExecuteSqlRawAsync(query);
        }
        /// <summary>
        /// Get Next Sequence Value By Sequence Name
        /// </summary>
        /// <param name="sequenceName"></param>
        /// <returns></returns>
        public long GetNextSequenceValue(string sequenceName)
        {
            var value = Context.GetNextSequenceValue(sequenceName);
            return value;
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<int> Count(Expression<Func<T, bool>> predicate = null) => predicate == null ? await DbSet.CountAsync() : await DbSet.CountAsync(predicate);
        /// <summary>
        /// Max
        /// </summary>
        /// <typeparam name="TB"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TB> Max<TB>(Expression<Func<T, TB>> selector, Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await DbSet.MaxAsync(selector);
            return await DbSet.Where(predicate).MaxAsync(selector);
        }
        /// <summary>
        /// Any
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> Any(Expression<Func<T, bool>> predicate = null) => predicate == null ? await DbSet.AnyAsync() : await DbSet.AnyAsync(predicate);
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        public T Add(T newEntity)
        {
            return DbSet.Add(newEntity).Entity;
        }
        /// <summary>
        /// Add Range
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="originalEntity"></param>
        /// <param name="newEntity"></param>
        public void Update(T originalEntity, T newEntity)
        {
            Context.Entry(originalEntity).CurrentValues.SetValues(newEntity);
        }
        /// <summary>
        /// Update Range
        /// </summary>
        /// <param name="newEntities"></param>
        public void UpdateRange(IEnumerable<T> newEntities)
        {
            Context.UpdateRange(newEntities);
        }
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
        /// <summary>
        /// Remove Logical
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveLogical(T entity)
        {
            var type = entity.GetType();
            var property = type.GetProperty("IsDeleted");
            if (property != null) property.SetValue(entity, true);
            var id = type.GetProperty("Id")?.GetValue(entity);
            var original = DbSet.Find(id);
            Update(original, entity);
        }
        /// <summary>
        /// Remove With Predicate
        /// </summary>
        /// <param name="predicate"></param>
        public async void Remove(Expression<Func<T, bool>> predicate)
        {
            var objects = await DbSet.FindAsync(predicate);
            DbSet.RemoveRange(objects);
        }
        /// <summary>
        /// Remove Range
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        #endregion

    }
}
