using System;
using System.Threading.Tasks;
using BackendCore.Common.Abstraction.Repository;

namespace BackendCore.Common.Abstraction.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IRepository<T> Repository { get; }
        Task<int> SaveChanges();
        void StartTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
