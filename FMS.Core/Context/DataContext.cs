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

        public DbSet<BillPayable> BillPayables { get; set; }
        public DbSet<BillReceivable> BillReceivable { get; set; }

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
