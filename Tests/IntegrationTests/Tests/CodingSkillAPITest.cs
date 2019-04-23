using System;
using System.Collections.Generic;
using System.Linq;
using IntegrationTests.Utils.Setup;
using Xunit;


namespace IntegrationTests
{
    /// <summary>
    /// Integration tests for the coding skill API.
    /// </summary>
    public class CosingSkillAPITest : BaseIntegrationTest
    {
        private readonly string API_URL = "api/coding-skill";

        public CosingSkillAPITest(APITestFactory<TestStartup> factory) : base(factory) { }

        [Fact]
        public async void GetCodingSkillsByFilter()
        {
            //var filter = new
            //{
            //    Name = "Testiasiakas",
            //    Status = "active"
            //};

            //List<Client> clients = await Get<List<Client>>(API_URL, filter);
            //Assert.True((clients != null && clients.Count() > 0), "No clients found from the AML API.");
        
        }

        [Fact]
        public async void GetAllCodingSkills()
        {
        }

        [Fact]
        public async void GetCodingSkillWithNoResults()
        {

        }

        [Fact]
        public async void GetCodingSkillById()
        {
        }

        [Fact]
        public async void GetNonExistingCodingSkillById()
        {

        }

        // TODO UPDATE
        // TODO DELETE
        // TODO ADD

    }
}