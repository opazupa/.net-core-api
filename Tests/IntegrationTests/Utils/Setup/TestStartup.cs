using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using API;

namespace IntegrationTests.Utils.Setup
{
    /// <summary>
    /// Extended class for test server from Startup
    /// </summary>
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Extend app service configurations.
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            
            // Disable possible authentication
            services.AddMvc(o => o.Filters.Add(new AllowAnonymousFilter()));
        }
    }
}
