using System;
using System.IO;
using System.Reflection;
using FeatureLibrary.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace API.Extensions
{
    /// <summary>
    /// Service extensions methods used in startup.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure cors.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        /// <summary>
        /// Configure database.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbConfig"></param>
        public static void ConfigureDatabase(this IServiceCollection services, DatabaseConfiguration dbConfig)
        {
            if (dbConfig.UseInMemoryDB)
            {
                services.AddDbContext<FeatureContext>(options => options.UseInMemoryDatabase(databaseName: "Olli's DB"));

            }
            else
            {
                // TODO not implemnented for API base.
            }
        }

        /// <summary>
        /// Configures swagger doc generation.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Olli's API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
