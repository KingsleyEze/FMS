using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using FMS.Core.Abstract;
using FMS.Core.Context;
using FMS.Core.Model;
using FMS.Core.Concrete;

namespace FMS.Core.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Privates
        readonly DataContext _context;
        bool _disposed;

        readonly Lazy<IRepository<BillPayable>> _billPayablesRepository;
        readonly Lazy<IRepository<BillReceivable>> _billReceivablesRepository;

        #endregion

        public UnitOfWork(DataContext context)
        {
            _context = context;

            #region Set Repository
            _billPayablesRepository = new Lazy<IRepository<BillPayable>>(() => new Repository<BillPayable>(context));
            _billReceivablesRepository = new Lazy<IRepository<BillReceivable>>(() => new Repository<BillReceivable>(context));
            
            #endregion
        }

        #region Getters
        public IRepository<BillPayable> BillPayablesRepository => _billPayablesRepository.Value;
        public IRepository<BillReceivable> BillReceivablesRepository => _billReceivablesRepository.Value;
        

        #endregion
        public bool IsDisposed => _disposed;

        public void SaveChanges() => _context.SaveChanges();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
        public async Task DbInitAsync() => await _context.Database.MigrateAsync();

        public async Task<IDbContextTransaction> TransactionAsync() => await _context.Database.BeginTransactionAsync();
        public IDbContextTransaction Transaction() => _context.Database.BeginTransaction();
    }
}
