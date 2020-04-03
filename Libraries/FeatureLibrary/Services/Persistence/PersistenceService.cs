using System.Threading.Tasks;
using FeatureLibrary.Database;

namespace FeatureLibrary.Services.Persistence
{
    /// <summary>
    /// Persistence service for db TXs.
    /// </summary>
    public class PersistenceService: IPersistenceService
    {
        private readonly FeatureContext _dbContext;
        public PersistenceService(FeatureContext context)
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
