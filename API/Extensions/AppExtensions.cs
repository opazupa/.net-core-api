using API.Middlewares;
using CoreLibrary.Configuration;
using FeatureLibrary.Models;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    /// <summary>
    /// App extension used in startup.
    /// </summary>
    public static class AppExtensions
    {
        /// <summary>
        /// Configure middlewares.
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureMiddlewares(this IApplicationBuilder app) => app.UseMiddleware<ExceptionHandlerMiddleware>();

        /// <summary>
        /// Configure development environment
        /// </summary>
        /// <param name="app"></param>
        /// <param name="databaseConfiguration"></param>
        public static void ConfigureDevelopmentEnvironment(this IApplicationBuilder app, DatabaseConfiguration databaseConfiguration)
        {
            app.UseDeveloperExceptionPage();
            app.ConfigureSwagger();
            // use graphql-playground middleware at default url /playground
            app.UsePlayground(new PlaygroundOptions
            {
                EnableSubscription = false,
                QueryPath = "/graphql",
                Path = "/playground"
            }) ;

            using var serviceScope = app.ApplicationServices.CreateScope();
            if (databaseConfiguration.UseInMemoryDB)
            {
                // Reset and seed the database.
                serviceScope.ServiceProvider.GetService<FeatureContext>().Database.EnsureCreatedAsync();
            }
            else
            {
                // Migrate the database.
                serviceScope.ServiceProvider.GetService<FeatureContext>().Database.Migrate();
            }
        }

        /// <summary>
        /// Configure swagger UI.
        /// </summary>
        /// <param name="app"></param>
        private static void ConfigureSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Olli's API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
