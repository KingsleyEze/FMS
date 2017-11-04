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
            BillPayable.ConfigureFluent(builder);
            BillReceivable.ConfigureFluent(builder);
        }
    }
}
