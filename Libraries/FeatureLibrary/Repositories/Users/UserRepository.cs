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
        /// Verify user with matching username and password
        /// TODO don't use in production
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public Task<UserEntity> Verify(Authentication auth)
        {
            return _context.Users
                .Where(u => u.UserName == auth.Username)
                .Where(u => u.Password == auth.Password)
                .Include(u => u.Skills)
                .SingleOrDefaultAsync();
        }
    }
}
