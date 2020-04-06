using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FeatureLibrary.Database.MockData;
using FeatureLibrary.Database;
using FeatureLibrary.Models;
using IntegrationTests.Utils.Setup;
using Xunit;
using CoreLibrary.Exceptions;

namespace IntegrationTests
{
    /// <summary>
    /// Integration tests for the auth API.
    /// </summary>
    [Collection("Sequential")]
    public class AuthAPITest : BaseIntegrationTest
    {
        private readonly string API_URL = "api/auth";

        [Fact]
        public async Task Login()
        {
            var admin = SeedData.ADMIN_USER;

            var auth = new Authentication()
            {
                Password = admin.Password,
                Username = admin.Name
            };

            var result = await Post<AuthenticationResult>($"{API_URL}/login", auth);
            Assert.True(result.UserId == admin.Id);
            Assert.True(result.Token != null);
        }

        [Fact]
        public async Task LoginUnknownUser()
        {
            var newUser = GetUsers(1).Select(u => new Authentication()
            {
                Password = u.Password,
                Username = u.Name
            }).First();

            await Assert.ThrowsAsync<BadRequestException>(() => Post<AuthenticationResult>($"{API_URL}/login", newUser));
        }

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