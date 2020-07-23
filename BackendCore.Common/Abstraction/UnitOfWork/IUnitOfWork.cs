using System;
using System.Threading.Tasks;
using BackendCore.Common.Abstraction.Repository;
using BackendCore.Entities.Entities.Base;

namespace BackendCore.Common.Abstraction.UnitOfWork
{
    public interface IUnitOfWork<T,TKey> : IDisposable where T : BaseEntity<TKey>
    {
        IRepository<T,TKey> Repository { get; }
        Task<int> SaveChanges();
        void StartTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
