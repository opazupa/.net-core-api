using System.Threading.Tasks;
using FeatureLibrary.Models;

namespace FeatureLibrary.Repositories
{
    public interface IUserRepository
    {
        Task<User> Add(Authentication auth);
        Task<User> Verify(Authentication auth);
    }
}