using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Models;

namespace FeatureLibrary.Services
{
    public interface IUserService
    {
        Task<AuthenticationResult> Authenticate(Authentication auth);
        Task<long> CreateUser(Authentication auth);
    }
}
