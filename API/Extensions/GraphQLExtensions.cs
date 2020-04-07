using Microsoft.Extensions.DependencyInjection;
using API.GraphQL;
using GraphQL;
using GraphQL.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace API.Extensions
{
    public static class GraphQLExtensions
    {

        /// <summary>
        /// Configure GraphQL.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureGraphQL(this IServiceCollection services, IWebHostEnvironment env)
        {
            // Queries and mutations
            services.AddSingleton<APIQuery>();
            services.AddSingleton<APIMutation>();

            // Types
            services.AddSingleton<UserType>();
            services.AddSingleton<CodingSkillType>();

            // Common ones
            services.AddGraphQL(opt =>
            {
                opt.EnableMetrics = env.IsDevelopment();
                opt.ExposeExceptions = env.IsDevelopment();
            });
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
        }
    }
}
