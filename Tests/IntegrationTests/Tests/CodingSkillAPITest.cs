using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using CoreLibrary.Exceptions;
using FeatureLibrary.Models;
using FeatureLibrary.Models.Entities;
using IntegrationTests.Utils.Setup;
using Xunit;


namespace IntegrationTests
{
    /// <summary>
    /// Integration tests for the coding skill API.
    /// </summary>
    [Collection("Sequential")]
    public class CodingSkillAPITest : BaseIntegrationTest
    {
        private readonly string API_URL = "api/coding-skill";

        [Fact]
        public async Task AddAndDeleteCodingSkill()
        {
            var newSkill = MockData.GetSkills(1).First();
            var skillId = await Post<long>(API_URL, newSkill);
            var skill = await Get<CodingSkill>($"{API_URL}/{skillId}");

            Assert.True(skill != null, $"Skill shouldn't be null.");
            Assert.True(skill.Id != 0, $"Skill shouldn't be 0.");
            Assert.True(skill.Name == newSkill.Name, $"Coding skill name doesn't match.");
            Assert.True(skill.Level == newSkill.Level, $"Coding skill level doesn't match.");

            await Delete<string>($"{API_URL}/{skill.Id}");
            NotFoundException ex = await Assert.ThrowsAsync<NotFoundException>(() => Get<CodingSkill>($"{API_URL}/{skill.Id}"));
        }

        [Fact]
        public async Task GetCodingSkillsByFilter()
        {
            var testSkill = (await Get<List<CodingSkill>>(API_URL)).First();
            var filter = new
            {
                Levels = testSkill.Level,
                Name = testSkill.Name
            };


            List<CodingSkill> skills = await Get<List<CodingSkill>>(API_URL, filter);
            Assert.True(skills != null && skills.Any(), "No skills found from the API.");
            Assert.True(skills.All(skill => filter.Levels == skill.Level), "Skills not matchig to level filter.");
            Assert.True(skills.All(skill => skill.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase)), "Skills not matchig to name filter.");
        }

        [Fact]
        public async Task GetAllCodingSkills()
        {
            List<CodingSkill> skills = await Get<List<CodingSkill>>(API_URL);
            Assert.True(skills != null && skills.Count() >= 10, "Not all seed data skills found from the API.");
        }

        [Fact]
        public async Task GetCodingSkillWithNoResults()
        {
            var filter = new CodingSkillFilter()
            {
                Name = new Guid().ToString()
            };

            List<CodingSkill> skills = await Get<List<CodingSkill>>(API_URL, filter);
            Assert.True(skills != null && !skills.Any(), "No skills should found from the API.");
        }

        [Fact]
        public async Task GetCodingSkillById()
        {
            var testSkill = (await Get<List<CodingSkill>>(API_URL)).First();    
            CodingSkill skill = await Get<CodingSkill>($"{API_URL}/{testSkill.Id}");

            Assert.True(skill != null, $"Test skill with id {testSkill.Id} should found from the API.");
            Assert.True(skill.Name == testSkill.Name, $"Found coding skill name doesn't match to testskill's.");
            Assert.True(skill.Level == testSkill.Level, $"Found coding skill level doesn't match to testskill's.");
        }

        [Fact]
        public async Task UpdateCodingSkill()
        {
            var testSkill = (await Get<List<CodingSkill>>(API_URL)).First();
            var modified = new ModifiedSkill
            {
                Name = testSkill.Name + "-334",
                Level = (int)testSkill.Level != 1 ? testSkill.Level - 1 : CodingSkillLevel.Master
            };

            CodingSkill updatedSkill = await Put<CodingSkill>($"{API_URL}/{testSkill.Id}", modified);
            
            Assert.True(updatedSkill != null, $"Test skill with id {testSkill.Id} should found from the API.");
            Assert.True(updatedSkill.Name == modified.Name, $"Found coding skill name doesn't match to updated one.");
            Assert.True(updatedSkill.Level == modified.Level, $"Found coding skill level doesn't match to updated one.");
        }

        [Fact]
        public async Task GetNonExistingCodingSkillById()
        {
            int unknownSkill = 9090909;
            NotFoundException ex = await Assert.ThrowsAsync<NotFoundException>(() => Get<CodingSkill>($"{API_URL}/{unknownSkill}"));
        }
    }
}