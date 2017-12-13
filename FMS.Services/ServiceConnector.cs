using System;
using System.Collections.Generic;
using System.Text;
using FMS.Core.Abstract;
using FMS.Core.Concrete;
using FMS.Services.Managers;
using FMS.Services.Managers.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace FMS.Services
{
    public static class ServiceConnector
    {
        public static void AddFMSServiceServices(this IServiceCollection services)
        {
            services.AddTransient<IBudgetManager, BudgetManager>();
            services.AddTransient<IPayableManager, PayableManager>();
            services.AddTransient<IReceivableManager, ReceivableManager>();
            services.AddTransient<ILineItemManager, LineItemManager>();
            services.AddTransient<IBankAccountManager, BankAccountManager>();
        }
    }
}
