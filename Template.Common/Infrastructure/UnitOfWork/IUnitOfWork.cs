using System;
using System.Threading.Tasks;
using Template.Common.Infrastructure.Repository;

namespace Template.Common.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        /// <summary>
        /// Repository Instance In Base Service
        /// </summary>
        IRepository<T> Repository { get; }
        /// <summary>
        /// Get Repository Instance Without Constructor Injection
        /// </summary>
        /// <typeparam name="TB"></typeparam>
        /// <returns></returns>
        IRepository<TB> GetRepository<TB>() where TB : class;
        /// <summary>
        /// Save Changes Async
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
        /// <summary>
        /// Save Changes
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// Start Transaction
        /// </summary>
        void StartTransaction();
        /// <summary>
        /// Commit Transaction
        /// </summary>
        void CommitTransaction();
        /// <summary>
        /// Rollback
        /// </summary>
        void Rollback();
    }
}
