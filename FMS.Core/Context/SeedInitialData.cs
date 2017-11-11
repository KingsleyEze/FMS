using FMS.Core.Abstract;
using FMS.Core.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        }
}
