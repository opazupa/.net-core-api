using FeatureLibrary.Database;
using Microsoft.AspNetCore.Builder;

namespace API.Extensions
{
    /// <summary>
    /// App extension used in startup.
    /// </summary>
    public static class AppExtensions
    {
        /// <summary>
        /// Configure swagger UI.
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureSwagger(this IApplicationBuilder app)
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
