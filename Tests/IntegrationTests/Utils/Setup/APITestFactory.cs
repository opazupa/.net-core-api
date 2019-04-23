using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Utils.Setup
{
    /// <summary>
    /// Extension to provide custom startup configuration for test server.
    /// </summary>
    /// <typeparam name="TEntryPoint"></typeparam>
    public class APITestFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        /// <summary>
        /// Apply custom Startup with WebHostBuilder.
        /// </summary>
        /// <returns></returns>
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(null)
                          .UseStartup<TEntryPoint>();
        }

        /// <summary>
        /// Extend default configuration.
        /// </summary>
        /// <param name="builder"></param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            // Use testing environment
            builder.UseEnvironment("Testing");
            builder.ConfigureAppConfiguration((ctx, conf) =>
            {
                // Include testing appsettings
                conf.SetBasePath(Directory.GetCurrentDirectory());
                conf.AddJsonFile($"appsettings.json", optional: false);
                conf.AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: false);
            });
            base.ConfigureWebHost(builder);
        }

    }
}