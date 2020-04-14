using System.Collections.Generic;
using Bogus;
using FeatureLibrary.Models.Entities;

namespace FeatureLibrary.Models
{
    public static class MockData
    {
        /// <summary>
        /// Get seed skills 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static IEnumerable<CodingSkillEntity> GetSkills(int amount, IEnumerable<UserEntity> users = null)
        {
            var ids = 999999900;
            var codingSkills = new Faker<CodingSkillEntity>()
                .StrictMode(false)
                .RuleFor(s => s.Id, f => ids++)
                .RuleFor(s => s.Name, n => $"{n.Hacker.Adjective()} {n.Hacker.Abbreviation()}")
                .RuleFor(s => s.Level, l => l.PickRandom<CodingSkillLevel>());

            if (users != null) codingSkills.RuleFor(s => s.UserId, n => n.PickRandom(users).Id);

            return codingSkills.Generate(amount);
        }

        /// <summary>
        /// Get seed users 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static IEnumerable<UserEntity> GetUsers(int amount)
        {
            var ids = 999999900;
            var users = new Faker<UserEntity>()
                .StrictMode(false)
                .RuleFor(s => s.Id, f => ids++)
                .RuleFor(s => s.UserName, n => n.Internet.UserName())
                .RuleFor(s => s.Password, s => s.Internet.Password());

            return users.Generate(amount);
        }
    }
}
