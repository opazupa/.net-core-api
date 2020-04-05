using System.Linq;
using System.Threading.Tasks;
using FeatureLibrary.Database;
using FeatureLibrary.Models;
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
        /// <param name="auth"></param>
        /// <returns></returns>
        public async Task<User> Add(Authentication auth)
        {
            var newUser = new User {
                Name = auth.Username,
                Password = auth.Password
            };

            await _context.Users.AddAsync(newUser);
            return newUser;
        }

        /// <summary>
        /// Verify user with matching username and password
        /// TODO don't use in production
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public Task<User> Verify(Authentication auth)
        {
            return _context.Users
                .Where(u => u.Name == auth.Username)
                .Where(u => u.Password == auth.Password)
                .FirstOrDefaultAsync();
        }
    }
}
