using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FeatureLibrary.Models;
using FeatureLibrary.Repositories;
using UnitTests.Utils.Setup;
using static FeatureLibrary.Database.MockData;
using Xunit;

namespace UnitTests.Services
{
    public class UserRepositoryTest
    {
        private readonly IEnumerable<User> testUsers = GetUsers(2);

        [Fact]
        public async Task VerifyUser()
        {
            var testUser = testUsers.Last();
            using var ctx = await DBContextHelper.ResetWithData(testUsers);
            IUserRepository repo = new UserRepository(ctx);

            var auth = new Authentication()
            {
                Password = testUser.Password,
                Username = testUser.Name
            };

            var user = await repo.Verify(auth);
            Assert.Equal(user.Id, testUser.Id);
            Assert.Equal(user.Name, testUser.Name);
            Assert.Equal(user.Password, testUser.Password);

        }

        [Fact]
        public async Task AddUser()
        {
            var newUser = GetUsers(1).Select(u => new Authentication()
            {
                Password = u.Password,
                Username = u.Name
            }).First();

            using var ctx = await DBContextHelper.ResetWithData(testUsers);
            IUserRepository repo = new UserRepository(ctx);

            var user = await repo.Add(newUser);
            Assert.True(user.Id != 0);
            Assert.Equal(user.Name, newUser.Username);
            Assert.Equal(user.Password, newUser.Password);
        }
    }
}
