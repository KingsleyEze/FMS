using FMS.Core.Abstract;
using FMS.Core.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Model;
using FMS.Utilities.Helpers;
using FMS.Utilities.StringKeys;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace FMS.Core.Context
{
    public static class SeedInitialData
    {
        public static async Task InitializeDatabaseAsync(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var unitOfWork = serviceScope.ServiceProvider.GetService<IUnitOfWork>();

                await unitOfWork.DbInitAsync();
                await CreateAdminUser(serviceProvider, unitOfWork);
                await InsertInitialData(serviceProvider);
            }
        }

        static async Task<bool> ShouldMigrate(DataContext db)
        {
            using (var connection = db.Database.GetDbConnection())
            {
                try
                {
                    await connection.OpenAsync();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        static async Task InsertInitialData(IServiceProvider serviceProvider)
        {
            await AddOrUpdateAsync(serviceProvider, g => g.Name, i => i.Id, DataToSeed.GetData.Countries);
            await AddOrUpdateAsync(serviceProvider, g => g.Name, i => i.Id, DataToSeed.GetData.States);
            await AddOrUpdateAsync(serviceProvider, g => g.Name, i => i.Id, DataToSeed.GetData.Banks);
        }

        private static async Task AddOrUpdateAsync<TEntity>(
            IServiceProvider serviceProvider,
            Func<TEntity, object> propertyToMatch, Func<TEntity, object> idProperty, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            List<TEntity> existingData;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<DataContext>();
                var dbSet = db.Set<TEntity>();
                existingData = dbSet.ToList();

                foreach (var item in entities)
                {
                    if (existingData.Any(g => propertyToMatch(g).Equals(propertyToMatch(item))))
                    {
                        var selectedData = existingData.SingleOrDefault(g => propertyToMatch(g).Equals(propertyToMatch(item)));
                        if (selectedData != null)
                        {
                        }
                    }
                    else
                    {

                        dbSet.Add(item);
                    }
                    await db.SaveChangesAsync();
                }
            }
        }

        static async Task CreateAdminUser(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
        {
            var env = serviceProvider.GetService<IHostingEnvironment>();
            
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();

            var defaultUser = DefaultUserKeys.DEFAULT_USER;
            var defaultPass = DefaultUserKeys.DEFAULT_PASS;
            
            var user = await userManager.FindByNameAsync(defaultUser);
         

            if (user == null)
            {
                user = new AppUser
                {
                    FirstName = "Admin",
                    LastName = "Super",
                    UserName = defaultUser,
                    Email = defaultUser,
                    EmailConfirmed = true,
                    UserType = "ADMIN"
                };
                var x = await userManager.CreateAsync(user, $"{defaultPass}");
            }
            

        }


    }
}
