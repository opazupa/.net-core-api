using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Exceptions;
using FeatureLibrary.Database;
using FeatureLibrary.Models;
using IntegrationTests.Utils.Setup;
using Xunit;


namespace IntegrationTests
{
    /// <summary>
    /// Integration tests for the coding skill API.
    /// </summary>
    public class CodingSkillAPITest : BaseIntegrationTest
    {
        private readonly string API_URL = "api/coding-skill";
        private readonly CodingSkill testSkill = SeedData.CodingSkills.First(); 

        public CodingSkillAPITest(APITestFactory<TestStartup> factory) : base(factory) { }

        [Fact]
        public async Task GetCodingSkillsByFilter()
        {
            var filter = new
            {
                Levels = CodingSkillLevel.Master,
                Name = "#"
            };


            List<CodingSkill> skills = await Get<List<CodingSkill>>(API_URL, filter);
            Assert.True((skills != null && skills.Any()), "No skills found from the API.");
            Assert.True(skills.All(skill => filter.Levels == skill.Level), "Skills not matchig to level filter.");
            Assert.True(skills.All(skill => skill.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase)), "Skills not matchig to name filter.");
        }

        [Fact]
        public async Task GetAllCodingSkills()
        {
            List<CodingSkill> skills = await Get<List<CodingSkill>>(API_URL);
            Assert.True((skills != null && skills.Count() == SeedData.CodingSkills.Count()), "Not all seed data skills found from the API.");
        }

        [Fact]
        public async Task GetCodingSkillWithNoResults()
        {
            var filter = new CodingSkillFilter()
            {
                Name = new Guid().ToString()
            };

            List<CodingSkill> skills = await Get<List<CodingSkill>>(API_URL, filter);
            Assert.True((skills != null && !skills.Any()), "No skills should found from the API.");
        }

        [Fact]
        public async Task GetCodingSkillById()
        {
            CodingSkill skill = await Get<CodingSkill>($"{API_URL}/{testSkill.Id}");
            Assert.True((skill != null), $"Test skill with id {testSkill.Id} should found from the API.");
            Assert.True((skill.Name == testSkill.Name), $"Found coding skill name doesn't match to testskill's.");
            Assert.True((skill.Level == testSkill.Level), $"Found coding skill level doesn't match to testskill's.");
        }

        [Fact]
        public async Task GetNonExistingCodingSkillById()
        {
            int unknownSkill = 9090909;
            NotFoundException ex = await Assert.ThrowsAsync<NotFoundException>(() => Get<CodingSkill>($"{API_URL}/{unknownSkill}"));
        }
    }
}