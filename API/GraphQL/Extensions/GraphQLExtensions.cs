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
                    .ModifyOptions(opt => opt.UseXmlDocumentation = true)
                    .AddType<UserType>()
                    .Create(),
                new QueryExecutionOptions
                {
                    //Defines that the execution engine shall be forced to execute serially
                    //since DbContext is not thread-safe.
                    ForceSerialExecution = true,
                    IncludeExceptionDetails = debugMode
                }) ;

            // Provide the user for auth token
            //services.AddQueryRequestInterceptor((context, builder, ct) =>
            //{
            //    if (context.User.Identity.IsAuthenticated)
            //    {
            //        builder.AddProperty("userId", context.User.GetId());
            //    }
            //    return Task.CompletedTask;
            //});

        }

        //private static string AddTypes(this ISchemaBuilder builder)
        //{
        //    builder.AddTypes()
        //}
    }
}
