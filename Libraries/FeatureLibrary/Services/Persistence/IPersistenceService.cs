using System.Threading.Tasks;

namespace FeatureLibrary.Services.Persistence
{
    public interface IPersistenceService
    {
        Task CompleteAsync();
    }
}
