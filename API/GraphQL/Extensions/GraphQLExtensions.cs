using API.GraphQL.Mutations;
using API.GraphQL.Queries;
using API.GraphQL.Subscriptions;
using HotChocolate; 
using HotChocolate.Execution.Configuration;
using HotChocolate.Subscriptions;
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
            // Subscriptions
            services.AddInMemorySubscriptionProvider();
            // Schema and common configs
            services.AddGraphQL(sp =>
                SchemaBuilder.New()
                    .AddServices(sp)
                    .AddQueryType<APIQuery>()
                    .AddMutationType<APIMutation>()
                    .AddSubscriptionType<APISubscription>()
                    .AddAuthorizeDirectiveType()
                    .Create(),
                new QueryExecutionOptions
                {
                    IncludeExceptionDetails = debugMode,
                    TracingPreference = debugMode ? TracingPreference.Always : TracingPreference.Never
                });
        }
    }
}
