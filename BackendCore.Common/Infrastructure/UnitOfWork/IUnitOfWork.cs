using System;
using System.Threading.Tasks;
using BackendCore.Common.Infrastructure.Repository;

namespace BackendCore.Common.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IRepository<T> Repository { get; }
        IRepository<TB> GetRepository<TB>() where TB : class;
        Task<int> SaveChanges();
        void StartTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
