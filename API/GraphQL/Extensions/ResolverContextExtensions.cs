using System.Threading.Tasks;
using CoreLibrary.Services.Persistence;
using FeatureLibrary.Extensions;
using HotChocolate.Resolvers;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Http;

namespace API.GraphQL.Extensions
{
    public static class ResolverContextExtensions
    {
        /// <summary>
        /// Save context (db transactions)
        /// </summary>
        /// <param name="ctx"></param>
        public static async Task Save(this IResolverContext ctx)
        {
            await ctx.Service<IPersistenceService>().CompleteAsync();    
        }

        /// <summary>
        /// Trigger an event for subscriptions
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="message"></param>
        public static async Task TriggerEvent<T>(this IResolverContext ctx, T  message) where T : IEventMessage
        {
            await ctx.Service<IEventSender>().SendAsync(message);    
        }

        /// <summary>
        /// Get user id from httpcontext
        /// </summary>
        /// <param name="ctx"></param>
        public static long? GetUserId(this IResolverContext ctx)
        {
            return ((HttpContext)ctx.ContextData[nameof(HttpContext)]).User.GetId();
        }
    }
}