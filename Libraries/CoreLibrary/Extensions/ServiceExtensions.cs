using System;
using CoreLibrary.Database;
using CoreLibrary.Services.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLibrary.Extensions
{
    public static class ServiceConfiguration
    {
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
                services.AddDbContext<T>(options => options.UseInMemoryDatabase(databaseName: $"Olli's {nameof(T)} DB"));
            }
            else
            {
                // TODO not implemented for API base.
                throw new NotImplementedException("Only in memory DB suppported.");
            }
        }


        /// <summary>
        /// Configures swagger doc generation.
        /// </summary>
        /// <param name="services"></param>
        //public static void ConfigureSwagger(this IServiceCollection services)
        //{
        //    // Register the Swagger generator, defining 1 or more Swagger documents
        //    //services.AddSwaggerGen(c =>
        //    //{
        //    //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Olli's API", Version = "v1" });

        //    //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //    //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //    //    c.IncludeXmlComments(xmlPath);
        //    //});
        //}
    }
}
