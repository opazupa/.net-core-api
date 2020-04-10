using API.GraphQL.Queries;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.GraphQL.Extensions
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
            // Dataloader
            services.AddDataLoaderRegistry();

            // Schema and common configs
            services.AddGraphQL(sp =>
                SchemaBuilder.New()
                    .AddServices(sp)
                    .AddQueryType<APIQuery>()
                    .AddAuthorizeDirectiveType()
                    .Create(),
                new QueryExecutionOptions
                {
                    IncludeExceptionDetails = debugMode
                });
        }
    }
}
