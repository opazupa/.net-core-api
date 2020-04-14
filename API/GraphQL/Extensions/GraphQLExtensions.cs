using API.GraphQL.Configuration;
using API.GraphQL.Mutations;
using API.GraphQL.Queries;
using API.GraphQL.Subscriptions;
using HotChocolate; 
using HotChocolate.Execution.Configuration;
using HotChocolate.Subscriptions;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace API.GraphQL.Extensions
{
    public static class GraphQLExtensions
    {

        /// <summary>
        /// Configure GraphQL.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="redisConfiguration"></param>
        /// <param name="debugMode"></param>
        public static void ConfigureGraphQL(this IServiceCollection services, RedisConfiguration redisConfiguration, bool debugMode = false)
        {
            // Dataloader
            services.AddDataLoaderRegistry();

            // Subscriptions
            if (redisConfiguration.UseInMemory)
            {
                services.AddInMemorySubscriptions();
            }
            else
            {
                var configuration = new ConfigurationOptions
                {
                    Ssl = redisConfiguration.Ssl,
                    AbortOnConnectFail = redisConfiguration.AbortOnConnectFail,
                    Password = redisConfiguration.Password
                };

                configuration.EndPoints.Add(redisConfiguration.Url);
                services.AddRedisSubscriptions(configuration);
            }

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
