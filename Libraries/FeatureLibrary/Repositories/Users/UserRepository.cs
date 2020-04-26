using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureLibrary.Models;
using FeatureLibrary.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeatureLibrary.Repositories
{
    /// <summary>
    /// User repository.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly FeatureContext _context;

        public UserRepository(FeatureContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<UserEntity> Add(UserEntity newUser)
        {
            await _context.Users.AddAsync(newUser);
            return newUser;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserEntity> GetById(long userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<UserEntity> GetByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _context.Users
                .OrderBy(u => u.UserName)
                .ToListAsync();
        }
    }
}
