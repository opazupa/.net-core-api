using System.Threading.Tasks;
using CoreLibrary.Services.Persistence;
using HotChocolate.Resolvers;

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
    }
}