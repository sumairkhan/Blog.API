using Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class StarttupDbExtension
    {
        public static void CreateDbIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var service = scope.ServiceProvider;
            var logger = service.GetRequiredService<ILogger<BlogDbContext>>();
            var blogContext = service.GetRequiredService<BlogDbContext>();
            try
            {
                var databasecrate = blogContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databasecrate != null)
                {
                    logger.LogInformation("enter databasecrate");
                    if (!databasecrate.CanConnect())
                    {
                        databasecrate.Create();
                        logger.LogInformation("enter databasecrate Create");
                    }

                    if (!databasecrate.HasTables())
                    {
                        databasecrate.CreateTables();
                    }
                    DbInitializerSeedData.InitializeDatabase(blogContext);
                    logger.LogInformation("enter databasecrate InitializeDatabase");
                }

            } catch (Exception ex) { logger.LogError($"migration issue {ex.Message} "); }  
        }
    }
}
