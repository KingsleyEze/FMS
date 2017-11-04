using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using FMS.Core.Model;

namespace FMS.Core.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<BillPayable> BillPayablesRepository { get; }
        IRepository<BillReceivable> BillReceivablesRepository { get; }


        bool IsDisposed { get; }
        void SaveChanges();
        Task SaveChangesAsync();
        Task DbInitAsync();
        Task<IDbContextTransaction> TransactionAsync();
        IDbContextTransaction Transaction();
    }
}
