using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using FMS.Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FMS.Core;
using FMS.Core.Model;
using FMS.Utilities.StringKeys;
using FMS.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;

namespace FMS
{
    public partial class Startup
    {
        IConfigurationRoot Configuration;

        private readonly Dictionary<string, List<Exception>> _exceptions;

        public Startup(IHostingEnvironment env)
        {
            _exceptions = new Dictionary<string, List<Exception>>
                           {
                             { ExceptionKeys.ExceptionsOnStartup, new List<Exception>() },
                             { ExceptionKeys.ExceptionsOnConfigureServices, new List<Exception>() },
                           };

            try
            {
                var builder = new ConfigurationBuilder()
                               .SetBasePath(env.ContentRootPath)
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                               .AddEnvironmentVariables();
                Configuration = builder.Build();

                //_mapperConfiguration = new MapperConfiguration(cfg =>
                //{
                //    cfg.AddProfile(new AutoMapperProfileConfiguration());
                //});
            }
            catch (Exception ex)
            {
                _exceptions[ExceptionKeys.ExceptionsOnStartup].Add(ex);
            }

            //Configuration = new ConfigurationBuilder()
            //.SetBasePath(env.ContentRootPath)
            //.AddJsonFile("appsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {

                services.AddDbContext<DataContext>(((options) =>
                                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"])), 
                                ServiceLifetime.Transient);

                services.AddIdentity<AppUser, IdentityRole>()
                                .AddEntityFrameworkStores<DataContext>();
                

                services.AddFMSCoreServices();
                services.AddMvc();
                services.AddDistributedMemoryCache();
                services.AddSession();
                

            }
            catch(Exception ex) {

                _exceptions[ExceptionKeys.ExceptionsOnConfigureServices].Add(ex);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            try
            {

                app.InitializeDatabaseAsync().Wait();
                app.UseSession();
                app.UseStatusCodePages();
                app.UseStaticFiles();
                app.UseIdentity();
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex);//_exceptions[ExceptionKeys.ExceptionsOnStartup].Add(ex);
            }

        }
    }
}
