using System.Threading.Tasks;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Models;

namespace FeatureLibrary.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> Add(UserEntity userEntity);
        Task<UserEntity> Verify(Authentication auth);
    }
}