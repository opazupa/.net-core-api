using Microsoft.Extensions.DependencyInjection;
using API.GraphQL;
using GraphQL;
using GraphQL.Server;

namespace API.Extensions
{
    public static class GraphQLExtensions
    {

        /// <summary>
        /// Configure GraphQL.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="debugMode"></param>
        public static void ConfigureGraphQL(this IServiceCollection services, bool debugMode = false)
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
                opt.EnableMetrics = debugMode;
                opt.ExposeExceptions = debugMode;
            });
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
        }
    }
}
