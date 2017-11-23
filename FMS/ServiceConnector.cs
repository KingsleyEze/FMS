using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Core.Context;
using FMS.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FMS
{
    public static class ServiceConnector
    {
        public static void AddFMSWebServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
                {
                })
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
        }
    }
}
