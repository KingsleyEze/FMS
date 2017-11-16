using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FMS.Core.Model;

namespace FMS.Core.Context
{
    public partial class DataContext : DbContext
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
