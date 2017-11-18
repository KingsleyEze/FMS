using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FMS.Core.Model;

namespace FMS.Core.Context
{
    public static class ContextConfigurator
    {
        public static void ConfigureAppModel(this ModelBuilder builder)
        {
            AppRole.ConfigureFluent(builder);
            AppUser.ConfigureFluent(builder);
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
        }
    }
}
