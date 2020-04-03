using FeatureLibrary.Repositories;
using FeatureLibrary.Services.Persistence;
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
            services.AddScoped<IPersistenceService, PersistenceService>();

            services.AddScoped<ICodingSkillRepository, CodingSkillRepository>();
            services.AddScoped<ICodingSkillService, CodingSkillService>();
        }
    }
}
