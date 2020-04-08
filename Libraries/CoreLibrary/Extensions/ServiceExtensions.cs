using System;
using CoreLibrary.Configuration;
using CoreLibrary.Services.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Extensions
{
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Logger factory
        /// </summary>
        private static readonly ILoggerFactory DBLoggerFactory = LoggerFactory.Create(builder => builder
            .AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information)
            .AddConsole());

        /// <summary>
        /// Configure database.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConfig"></param>
        public static void ConfigureDatabase<T>(this IServiceCollection services, DatabaseConfiguration dbConfig) where T : DbContext
        {
            // Configure persistence context for unit of work.
            services.AddScoped<IPersistenceService, PersistenceService<T>>();

            if (dbConfig.UseInMemoryDB)
            {
                services.AddDbContext<T>(options => options
                    .UseInMemoryDatabase(databaseName: $"Olli's {nameof(T)} DB")
                    .UseLoggerFactory(DBLoggerFactory));
            }
            else
            {
                // TODO not implemented for API base.
                throw new NotImplementedException("Only in memory DB suppported.");
            }
        }
    }
}
