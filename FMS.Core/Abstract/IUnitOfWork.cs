using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using FMS.Core.Model;

namespace FMS.Core.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AppUser> AppUsersRepository { get; }
        IRepository<AppUserProfile> AppUserProfilesRepository { get; }
        IRepository<AppRole> AppRolesRepository { get; }
        IRepository<AppUserRole> AppUserRolesRepository { get; }
        IRepository<AppUserFile> AppUserFilesRepository { get; }
        IRepository<AppUserBank> AppUserBanksRepository { get; }
        IRepository<Bank> BanksRepository { get; }
        IRepository<Staff> StaffsRepository { get; }
        IRepository<Supplier> SuppliersRepository { get; }
        IRepository<Country> CountriesRepository { get; }
        IRepository<State> StatesRepository { get; }
        IRepository<LGA> LGAsRepository { get; }
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
