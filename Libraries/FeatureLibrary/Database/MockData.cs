using System.Collections.Generic;
using Bogus;
using FeatureLibrary.Models;

namespace FeatureLibrary.Database
{
    public static class MockData
    {
        /// <summary>
        /// Get seed skills 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static IEnumerable<CodingSkill> GetSkills(int amount)
        {
            var codingSkills = new Faker<CodingSkill>()
                .StrictMode(true)
                .RuleFor(s => s.Id, f => f.Random.Number(1, 10000000))
                .RuleFor(s => s.Name, n => n.Lorem.Sentence())
                .RuleFor(s => s.Level, l => l.PickRandom<CodingSkillLevel>());

            return codingSkills.Generate(amount);
        }
    }
}
