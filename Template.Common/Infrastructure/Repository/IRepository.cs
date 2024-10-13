using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Template.Common.Extensions;

namespace Template.Common.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<T> GetAsync(params object[] keys);
        /// <summary>
        /// Return DbSet As Queryable
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> predicate = null);
        /// <summary>
        /// First Or Default
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="orderByCriteria"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        /// <summary>
        /// Find Async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderByCriteria"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
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
        Task<(int, IEnumerable<T>)> FindPagedAsync(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        /// <summary>
        /// Get Select
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="where"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        Task<ICollection<TType>> GetSelectAsync<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select) where TType : class;
        /// <summary>
        /// Find Select
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="select"></param>
        /// <param name="predicate"></param>
        /// <param name="orderByCriteria"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        Task<IEnumerable<TType>> FindSelectAsync<TType>(Expression<Func<T, TType>> select,
            Expression<Func<T, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
            where TType : class;
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
        Task<(int, IEnumerable<TType>)> FindPagedSelectAsync<TType>(Expression<Func<T, TType>> select,
            Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0,
            IEnumerable<SortModel> orderByCriteria = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
            where TType : class;
        /// <summary>
        /// Find Grouped With Sort And Select
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
        IList<TReturn> FindGrouped<TResult, TKey, TGroup, TReturn>(
            List<Expression<Func<T, bool>>> predicates,
            Expression<Func<T, TResult>> firstSelector,
            Expression<Func<TResult, TKey>> orderSelector,
            Func<TResult, TGroup> groupSelector,
            Func<IGrouping<TGroup, TResult>, TReturn> selector, bool isDesc = false,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int skip = 0, int take = int.MaxValue);
        /// <summary>
        /// Execute Stored Procedure
        /// </summary>
        /// <typeparam name="TB"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<TB> ExecuteStored<TB>(string sql) where TB : class;
        /// <summary>
        /// Exec With Stored
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<int> ExecWithStoreProcedure(string query);
        /// <summary>
        /// Get Next Sequence Number
        /// </summary>
        /// <param name="sequenceName"></param>
        /// <returns></returns>
        long GetNextSequenceValue(string sequenceName);
        /// <summary>
        /// Any
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> Any(Expression<Func<T, bool>> predicate = null);
        /// <summary>
        /// Count
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> Count(Expression<Func<T, bool>> predicate = null);
        /// <summary>
        /// Max
        /// </summary>
        /// <typeparam name="TB"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TB> Max<TB>(Expression<Func<T, TB>> selector, Expression<Func<T, bool>> predicate = null);
        /// <summary>
        /// union Operator
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        IQueryable<T> Union(params IQueryable<T>[] queries);
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        T Add(T newEntity);

        /// <summary>
        /// Add Async
        /// </summary>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T newEntity);

        /// <summary>
        /// Add Range
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<T> entities);
        /// <summary>
        /// Add Range Async
        /// </summary>
        /// <param name="entities"></param>
        Task AddRangeAsync(IEnumerable<T> entities);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="originalEntity"></param>
        /// <param name="newEntity"></param>
        void Update(T originalEntity, T newEntity);
        /// <summary>
        /// Update Async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        Task UpdateAsync(object id, T newEntity);
        /// <summary>
        /// Update Range
        /// </summary>
        /// <param name="newEntities"></param>
        void UpdateRange(IEnumerable<T> newEntities);
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);
        /// <summary>
        /// Remove Logical
        /// </summary>
        /// <param name="entity"></param>
        void RemoveLogical(T entity);
        /// <summary>
        /// Remove With Condition
        /// </summary>
        /// <param name="predicate"></param>
        void Remove(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Remove Range
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<T> entities);
    }
}
