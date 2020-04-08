using Microsoft.Extensions.DependencyInjection;
using GraphQL;
using GraphQL.Server;

namespace API.GraphQL
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
            // Schema
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<APISchema>();

            // Common ones and types
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddGraphQL(opt =>
            {
                opt.EnableMetrics = debugMode;
                opt.ExposeExceptions = debugMode;
            })
            .AddUserContextBuilder(httpContext => httpContext.User)
            .AddGraphTypes(ServiceLifetime.Scoped);
        }
    }
}
