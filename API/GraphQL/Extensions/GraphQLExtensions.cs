using System.Security.Claims;
using System.Threading.Tasks;
using API.GraphQL.Queries;
using API.GraphQL.Types;
using FeatureLibrary.Extensions;
using HotChocolate;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
            services.AddDataLoaderRegistry();
            services.AddGraphQL(sp =>
                SchemaBuilder.New()
                    .AddServices(sp)
                    .AddQueryType<APIQuery>()
                    .Create(),
                new QueryExecutionOptions
                {
                    IncludeExceptionDetails = debugMode
                });
        }
    }
}
