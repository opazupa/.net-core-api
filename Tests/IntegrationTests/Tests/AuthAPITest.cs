using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FeatureLibrary.Database.MockData;
using FeatureLibrary.Database;
using FeatureLibrary.Models;
using IntegrationTests.Utils.Setup;
using Xunit;


namespace IntegrationTests
{
    /// <summary>
    /// Integration tests for the auth API.
    /// </summary>
    public class AuthAPITest : BaseIntegrationTest
    {
        private readonly string API_URL = "api/auth";

        [Fact]
        public async Task RegisterUser()
        {
            var newUser = GetUsers(1).Select(u => new Authentication()
            {
                Password = u.Password,
                Username = u.Name
            }).First();

            var userId = await Post<long>($"{API_URL}/register", newUser);
            Assert.True(userId != 0);
       }
    }
}