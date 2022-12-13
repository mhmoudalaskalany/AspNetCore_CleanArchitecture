using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Template.Common.Infrastructure.Repository;
using Template.Common.Infrastructure.UnitOfWork;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class 
    {
        private DbContext _context;
        private IDbContextTransaction _transaction;
        private Dictionary<string, dynamic> _repositories;
        public IRepository<T> Repository { get; }
        public UnitOfWork(DbContext context)
        {
            _context = context;
            Repository = new Repository<T>(_context);
        }

        #region Public Methods
        /// <summary>
        /// Get Repository Instance
        /// </summary>
        /// <typeparam name="TB"></typeparam>
        /// <returns></returns>
        public IRepository<TB> GetRepository<TB>() where TB : class
        {
            _repositories ??= new Dictionary<string, dynamic>();
            var type = typeof(TB).Name;
            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TB>)_repositories[type];
            }

            var repositoryType = typeof(Repository<TB>);
            var repository = Activator.CreateInstance(repositoryType, _context);
            _repositories.Add(type, repository);
            return _repositories[type];
        }
        /// <summary>
        /// Save Changes Async
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Save Changes
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return  _context.SaveChanges();
        }
        /// <summary>
        /// Start Transaction
        /// </summary>
        public void StartTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }
        /// <summary>
        /// Commit Transaction
        /// </summary>
        public void CommitTransaction()
        {
            _transaction.Commit();
            _transaction.Dispose();
        }
        /// <summary>
        /// Rollback
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
        /// <summary>
        /// Dispose Resource
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
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

        #endregion

    }
}