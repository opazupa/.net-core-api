using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Exceptions;
using FeatureLibrary.Models;
using IntegrationTests.Utils.Setup;
using Xunit;
using static FeatureLibrary.Models.MockData;

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
                Username = admin.UserName
            };

            var result = await Post<AuthenticationResult>($"{API_URL}/login", auth);
            Assert.True(result.UserName == admin.UserName);
            Assert.True(result.Token != null);
            Assert.True(result.TokenType == TokenType.Bearer);
            Assert.True(result.ExpiresIn.Date == DateTime.UtcNow.AddDays(7).Date);
        }

        [Fact]
        public async Task LoginUnknownUser()
        {
            var newUser = GetUsers(1).Select(u => new Authentication()
            {
                Password = u.Password,
                Username = u.UserName
            }).First();

            await Assert.ThrowsAsync<UnauthorizedException>(() => Post<AuthenticationResult>($"{API_URL}/login", newUser));
        }

        [Fact]
        public async Task RegisterUser()
        {
            var newUser = GetUsers(1).Select(u => new Authentication()
            {
                Password = u.Password,
                Username = u.UserName
            }).First();

            var userId = await Post<long>($"{API_URL}/register", newUser);
            Assert.True(userId != 0);
        }
    }
}