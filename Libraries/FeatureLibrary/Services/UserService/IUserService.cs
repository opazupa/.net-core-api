using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Models;
using FeatureLibrary.Models.Entities;

namespace FeatureLibrary.Services
{
    public interface IUserService
    {
        Task<AuthenticationResult> Authenticate(Authentication auth);
        Task<long> CreateUser(Authentication auth);
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity> GetById(long userId);
    }
}
