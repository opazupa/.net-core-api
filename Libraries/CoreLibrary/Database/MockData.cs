using System.Collections.Generic;
using System.Linq;
using Bogus;
using CoreLibrary.Models;

namespace CoreLibrary.Database
{
    public static class MockData
    {
        /// <summary>
        /// Admin user for testing
        /// </summary>
        private static readonly User ADMIN_USER = new User
        {
            Id = 1,
            Name = "admin",
            Password = "admin"
        };

        /// <summary>
        /// Get seed users 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static IEnumerable<User> GetUsers(int amount)
        {
            var ids = 2;
            var users = new Faker<User>()
                .StrictMode(true)
                .RuleFor(s => s.Id, f => ids++)
                .RuleFor(s => s.Name, n => n.Internet.UserName())
                .RuleFor(s => s.Password, s => s.Internet.Password());

            return users.Generate(amount).
                Concat(new[] { ADMIN_USER });
        }
    }
}
