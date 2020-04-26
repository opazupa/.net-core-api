using System.Threading.Tasks;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Models;
using System.Collections.Generic;

namespace FeatureLibrary.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> Add(UserEntity userEntity);
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity> GetById(long userId);
        Task<UserEntity> Verify(Authentication auth);
    }
}