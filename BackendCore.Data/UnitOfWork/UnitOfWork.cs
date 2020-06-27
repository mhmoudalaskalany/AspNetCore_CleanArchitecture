using System;
using System.Threading.Tasks;
using BackendCore.Common.Abstraction.Repository;
using BackendCore.Common.Abstraction.UnitOfWork;
using BackendCore.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BackendCore.Data.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private DbContext _context;
        private IDbContextTransaction _transaction;
        public IRepository<T> Repository { get; }
        public UnitOfWork(DbContext context)
        {
            _context = context;
            Repository = new Repository<T>(_context);
        }
        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
        public void StartTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            _transaction.Commit();
            _transaction.Dispose();
        }
        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_context == null)
            {
                return;
            }

            _context.Dispose();
            _context = null;
        }
    }
}