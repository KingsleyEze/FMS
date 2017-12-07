using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FMS.Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FMS.Core.Context
{
    public partial class DataContext : IdentityDbContext<AppUser>
    {
        //private readonly IHttpContextAccessor _contextAccessor;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //_contextAccessor = contextAccessor;
        }

        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserProfile> AppUserProfiles { get; set; }
        public DbSet<AppUserBank> AppUserBanks { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<AppUserFile> AppUserFiles { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<LGA> LGAs { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<BillPayable> BillPayables { get; set; }
        public DbSet<BillReceivable> BillReceivable { get; set; }
        public DbSet<Journal> Journal { get; set; }
        public DbSet<JournalLineItem> JournalLineItem { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<PayableWorkFlow> PayableWorkFlows { get; set; }
        public DbSet<ReceivableWorkFlow> ReceivableWorkFlows { get; set; }
        public DbSet<AccountGroup> AccountGroups { get; set; }
        public DbSet<AccountSubType> AccountSubTypes { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetAmendHistory> BudgetAmendHistories { get; set; }
        public DbSet<AppData> AppData { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureAppModel();

            var foreignKeys = builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in foreignKeys)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
