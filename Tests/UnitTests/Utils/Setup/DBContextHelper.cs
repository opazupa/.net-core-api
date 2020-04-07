using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Utils.Setup
{
    public static class DBContextHelper
    {
        /// <summary>
        /// Resets the in memory db with given data.
        /// </summary>
        /// <returns>In memory db context</returns>
        /// <param name="data"></param>
        public async static Task<FeatureContext> ResetWithData(IEnumerable<object> data)
        {
            var ctx = Init();
            await ctx.Database.EnsureDeletedAsync();
            await ctx.AddRangeAsync(data);
            await ctx.SaveChangesAsync();
            return ctx;
        }

        /// <summary>
        /// Init in memory db options.
        /// </summary>
        /// <returns></returns>
        private static FeatureContext Init()
        {
            var dbOptions = new DbContextOptionsBuilder<FeatureContext>()
                                .UseInMemoryDatabase(databaseName: new Guid().ToString())
                                .Options;
            return new FeatureContext(dbOptions);
        }
    }
}
