using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLibrary.Exceptions;
using FeatureLibrary.Models;
using FeatureLibrary.Repositories;

namespace FeatureLibrary.Services
{
    /// <summary>
    /// User service.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public async Task<long> CreateUser(Authentication auth)
        {
            var user = await _userRepository.Add(auth);
            return user.Id;
        }
    }
}
