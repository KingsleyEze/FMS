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

        readonly Lazy<IRepository<AppUser>> _appUsersRepository;
        readonly Lazy<IRepository<AppUserProfile>> _appUserProfilesRepository;
        readonly Lazy<IRepository<AppUserRole>> _appUserRolesRepository;
        readonly Lazy<IRepository<AppUserBank>> _appUserBanksRepository;
        readonly Lazy<IRepository<AppUserFile>> _appUserFilesRepository;
        readonly Lazy<IRepository<AppRole>> _appRolesRepository;
        readonly Lazy<IRepository<Bank>> _banksRepository;
        readonly Lazy<IRepository<Staff>> _staffsRepository;
        readonly Lazy<IRepository<Supplier>> _suppliersRepository;
        readonly Lazy<IRepository<Country>> _countriesRepository;
        readonly Lazy<IRepository<State>> _statesRepository;
        readonly Lazy<IRepository<LGA>> _lGAsRepository;
        readonly Lazy<IRepository<BillPayable>> _billPayablesRepository;
        readonly Lazy<IRepository<BillReceivable>> _billReceivablesRepository;

        #endregion

        public UnitOfWork(DataContext context)
        {
            _context = context;

            #region Set Repository
            _appUsersRepository = new Lazy<IRepository<AppUser>>(() => new Repository<AppUser>(context));
            _appUserProfilesRepository = new Lazy<IRepository<AppUserProfile>>(() => new Repository<AppUserProfile>(context));
            _appUserRolesRepository = new Lazy<IRepository<AppUserRole>>(() => new Repository<AppUserRole>(context));
            _appUserBanksRepository = new Lazy<IRepository<AppUserBank>>(() => new Repository<AppUserBank>(context));
            _appUserFilesRepository = new Lazy<IRepository<AppUserFile>>(() => new Repository<AppUserFile>(context));
            _appRolesRepository = new Lazy<IRepository<AppRole>>(() => new Repository<AppRole>(context));
            _banksRepository = new Lazy<IRepository<Bank>>(() => new Repository<Bank>(context));
            _staffsRepository = new Lazy<IRepository<Staff>>(() => new Repository<Staff>(context));
            _suppliersRepository = new Lazy<IRepository<Supplier>>(() => new Repository<Supplier>(context));
            _countriesRepository = new Lazy<IRepository<Country>>(() => new Repository<Country>(context));
            _statesRepository = new Lazy<IRepository<State>>(() => new Repository<State>(context));
            _lGAsRepository = new Lazy<IRepository<LGA>>(() => new Repository<LGA>(context));
            _billPayablesRepository = new Lazy<IRepository<BillPayable>>(() => new Repository<BillPayable>(context));
            _billReceivablesRepository = new Lazy<IRepository<BillReceivable>>(() => new Repository<BillReceivable>(context));
            
            #endregion
        }

        #region Getters
        public IRepository<AppUser> AppUsersRepository => _appUsersRepository.Value;
        public IRepository<AppUserProfile> AppUserProfilesRepository => _appUserProfilesRepository.Value;
        public IRepository<AppUserRole> AppUserRolesRepository => _appUserRolesRepository.Value;
        public IRepository<AppUserBank> AppUserBanksRepository => _appUserBanksRepository.Value;
        public IRepository<AppUserFile> AppUserFilesRepository => _appUserFilesRepository.Value;
        public IRepository<Bank> BanksRepository => _banksRepository.Value;
        public IRepository<AppRole> AppRolesRepository => _appRolesRepository.Value;
        public IRepository<Country> CountrysRepository => _countriesRepository.Value;
        public IRepository<State> StatesRepository => _statesRepository.Value;
        public IRepository<LGA> LGAsRepository => _lGAsRepository.Value;
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
