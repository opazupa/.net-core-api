using System.Collections.Generic;
using Bogus;
using FeatureLibrary.Models;

namespace UnitTests.Mocks
{
    public static class TestMocks
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static IEnumerable<CodingSkill> GetSkills(int amount)
        {
            var ids = 0;
            var codingSkills = new Faker<CodingSkill>()
                .StrictMode(true)
                .RuleFor(s => s.Id, ids++)
                .RuleFor(s => s.Name, n => n.Commerce.Product())
                .RuleFor(s => s.Level, l => l.PickRandom<CodingSkillLevel>());

            return codingSkills.Generate(amount);
        }

    }
}
