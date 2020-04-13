using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Exceptions;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Repositories;
using FeatureLibrary.Services;
using Moq;
using Xunit;
using static FeatureLibrary.Models.MockData;

namespace UnitTests.Services
{
    public class CodingSkillServiceTest
    {

        [Theory]
        [InlineData("name", CodingSkillLevel.Beginner, null)]
        [InlineData(null, CodingSkillLevel.Beginner, 234)]
        [InlineData("name", null, 234)]
        public async Task AddBadCodingSkill(string name, CodingSkillLevel? level, long? userId)
        {
            var toAdd = new CodingSkillEntity()
            {
                Name = name,
                Level = level ?? 0
            };

            var _repository = new Mock<ICodingSkillRepository>();
            var codingSkillService = new CodingSkillService(_repository.Object);

            await Assert.ThrowsAsync<BadRequestException>(() => codingSkillService.Add(toAdd, userId));
        }
    }
}
