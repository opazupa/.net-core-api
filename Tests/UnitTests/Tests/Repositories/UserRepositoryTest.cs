using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FeatureLibrary.Models;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Repositories;
using UnitTests.Utils.Setup;
using static FeatureLibrary.Models.MockData;
using Xunit;

namespace UnitTests.Repositories
{
    [Collection("Sequential")]
    public class UserRepositoryTest
    {
        private readonly IEnumerable<UserEntity> testUsers = GetUsers(2);

        [Fact]
        public async Task VerifyUser()
        {
            var testUser = testUsers.Last();
            using var ctx = await DBContextHelper.ResetWithData(testUsers);
            IUserRepository repo = new UserRepository(ctx);

            var auth = new Authentication()
            {
                Password = testUser.Password,
                Username = testUser.UserName
            };

            var user = await repo.Verify(auth);
            Assert.Equal(user.Id, testUser.Id);
            Assert.Equal(user.UserName, testUser.UserName);
            Assert.Equal(user.Password, testUser.Password);

        }

        [Fact]
        public async Task AddUser()
        {
            var newUser = GetUsers(1).First();
            newUser.Id = 0;

            using var ctx = await DBContextHelper.ResetWithData(testUsers);
            IUserRepository repo = new UserRepository(ctx);

            var user = await repo.Add(newUser);
            Assert.True(user.Id != 0);
            Assert.Equal(user.UserName, newUser.UserName);
            Assert.Equal(user.Password, newUser.Password);
        }
    }
}
