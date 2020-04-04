using System.Threading.Tasks;

namespace CoreLibrary.Services.Persistence
{
    public interface IPersistenceService
    {
        Task CompleteAsync();
    }
}
