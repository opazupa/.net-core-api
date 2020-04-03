using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FeatureLibrary.Models;
using FeatureLibrary.Repositories;
using UnitTests.Utils.Setup;
using static UnitTests.Mocks.TestMocks;
using Xunit;

namespace UnitTests.Services
{
    public class CodingSkillRepositoryTest
    {
        private readonly IEnumerable<CodingSkill> testSkills = GetSkills(2);


        [Fact]
        public async Task GetSkillById()
        {
            using var ctx = await DBContextHelper.ResetWithData(testSkills);
            ICodingSkillRepository repo = new CodingSkillRepository(ctx);

            CodingSkill skill = await repo.GetById(testSkills.First().Id);

            Assert.Equal(skill.Id, testSkills.First().Id);
            Assert.Equal(skill.Name, testSkills.First().Name);
            Assert.Equal(skill.Level, testSkills.First().Level);
        }

        [Fact]
        public async Task GetSkillByLevel()
        {
            using var ctx = await DBContextHelper.ResetWithData(testSkills);
            ICodingSkillRepository repo = new CodingSkillRepository(ctx); var filter = new CodingSkillFilter()
            {
                Levels = testSkills.Select(s => s.Level).Take(1).ToList()
            };
            IEnumerable<CodingSkill> skills = await repo.GetByFilter(filter);

            Assert.All(skills, skill => filter.Levels.Contains(skill.Level));
            Assert.Equal(skills, testSkills.Take(1));
        }
    }
}
