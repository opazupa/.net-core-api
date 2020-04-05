using System.Collections.Generic;
using System.Linq;
using Bogus;
using FeatureLibrary.Models;

namespace FeatureLibrary.Database
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
        /// Get seed skills 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static IEnumerable<CodingSkill> GetSkills(int amount, IEnumerable<User> users = null)
        {
            var codingSkills = new Faker<CodingSkill>()
                .StrictMode(false)
                .RuleFor(s => s.Id, f => f.Random.Number(1, 10000000))
                .RuleFor(s => s.Name, n => n.Lorem.Sentence())
                .RuleFor(s => s.Level, l => l.PickRandom<CodingSkillLevel>());

            if (users != null) codingSkills.RuleFor(s => s.UserId, n => n.PickRandom(users).Id);

            return codingSkills.Generate(amount);
        }

        /// <summary>
        /// Get seed users 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static IEnumerable<User> GetUsers(int amount)
        {
            var ids = 2;
            var users = new Faker<User>()
                .StrictMode(false)
                .RuleFor(s => s.Id, f => ids++)
                .RuleFor(s => s.Name, n => n.Internet.UserName())
                .RuleFor(s => s.Password, s => s.Internet.Password());

            return users.Generate(amount)
                .Concat(new[] { ADMIN_USER });
        }
    }
}
