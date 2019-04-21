
using FeatureLibrary.Repositories;
using FeatureLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FeatureLibrary.Services
{
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Configure services.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureFeatureServices(this IServiceCollection services)
        {
            services.AddScoped<ICodingSkillRepository, CodingSkillRepository>();
            services.AddScoped<ICodingSkillService, CodingSkillService>();
        }
    }
}
