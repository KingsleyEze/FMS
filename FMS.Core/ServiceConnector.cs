using System;
using Microsoft.Extensions.DependencyInjection;
using FMS.Core.Abstract;
using FMS.Core.Concrete;
using FMS.Core.Context;
using FMS.Core.Model;

namespace FMS.Core
{
    public static class ServiceConnector
    {
        public static void AddFMSCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

        }
    }
}
