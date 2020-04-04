using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.Services.Persistence
{
    /// <summary>
    /// Persistence service for db TXs.
    /// </summary>
    public class PersistenceService<T> : IPersistenceService where T : DbContext
    {
        private readonly T _dbContext;
        public PersistenceService(T context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Try to save made changes to database.
        /// </summary>
        /// <returns></returns>
        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
