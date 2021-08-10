using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BackendCore.Common.Extensions;
using Microsoft.EntityFrameworkCore.Query;

namespace BackendCore.Common.Abstraction.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(params object[] keys);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        Task<IEnumerable<T>> GetAllAsync(IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);

        Task<(int, IEnumerable<T>)> FindPagedAsync(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        Task<ICollection<TType>> GetSelectAsync<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select) where TType : class;

        Task<IEnumerable<TType>> FindSelectAsync<TType>(Expression<Func<T, TType>> select,
            Expression<Func<T, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
            where TType : class;
        Task<(int, IEnumerable<TType>)> FindPagedSelectAsync<TType>(Expression<Func<T, TType>> select,
            Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0,
            IEnumerable<SortModel> orderByCriteria = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
            where TType : class;
        Task<bool> Any(Expression<Func<T, bool>> predicate = null);
        Task<int> Count(Expression<Func<T, bool>> predicate = null);
        Task<TB> Max<TB>(Expression<Func<T, TB>> selector, Expression<Func<T, bool>> predicate = null);
        T Add(T newEntity);
        void AddRange(IEnumerable<T> entities);
        void Update(T originalEntity, T newEntity);
        void UpdateRange(IEnumerable<T> newEntities);
        void Remove(T entity);
        void RemoveLogical(T entity);
        void Remove(Expression<Func<T, bool>> predicate);
        void RemoveRange(IEnumerable<T> entities);
    }
}
