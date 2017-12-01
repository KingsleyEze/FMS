using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FMS.Core.Model;
using Microsoft.AspNetCore.Identity;

namespace FMS.Core.Context
{
    public static class ContextConfigurator
    {
        public static void ConfigureAppModel(this ModelBuilder builder)
        {
        
            AppUserProfile.ConfigureFluent(builder);
            AppUserBank.ConfigureFluent(builder);
            AppUserRole.ConfigureFluent(builder);
            AppUserFile.ConfigureFluent(builder);
            Bank.ConfigureFluent(builder);
            Country.ConfigureFluent(builder);
            State.ConfigureFluent(builder);
            LGA.ConfigureFluent(builder);
            Staff.ConfigureFluent(builder);
            Supplier.ConfigureFluent(builder);
            BillPayable.ConfigureFluent(builder);
            BillReceivable.ConfigureFluent(builder);
            Journal.ConfigureFluent(builder);
            JournalLineItem.ConfigureFluent(builder);
            Payment.ConfigureFluent(builder);
            Receipt.ConfigureFluent(builder);
            PayableWorkFlow.ConfigureFluent(builder);
            ReceivableWorkFlow.ConfigureFluent(builder);
            AccountGroup.ConfigureFluent(builder);
            AccountSubType.ConfigureFluent(builder);
            LineItem.ConfigureFluent(builder);
            BankAccount.ConfigureFluent(builder);

        }

        public static void ConfigureAppIdentity(this ModelBuilder builder)
        {
            builder.Entity<AppRole>().ToTable("AdminRoles");
            builder.Entity<IdentityRole>().ToTable("AdminRoles");
            builder.Entity<AppUser>().ToTable("AdminUsers");
            builder.Entity<IdentityUserRole<string>>().ToTable("AdminUserRoles").HasIndex(s => s.UserId);
            builder.Entity<IdentityUserLogin<string>>().ToTable("AdminUserLogins");
            builder.Entity<IdentityUserClaim<string>>().ToTable("AdminUserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("AdminRoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("AdminUserTokens");
        }
    }
}
