using System.Threading.Tasks;
using CoreLibrary.Exceptions;
using FeatureLibrary.Models;
using FeatureLibrary.Repositories;
using FeatureLibrary.Services;
using Moq;
using Xunit;

namespace UnitTests.Services
{
    public class CodingSkillServiceTest
    {

        [Fact]
        public async Task AddCodingSkillWithNoLevel()
        {
            var toAdd = new CodingSkill() { Name = "Angular", Id = 3 };
            var _repository = new Mock<ICodingSkillRepository>();
            _repository.Setup(x => x.Add(null)).Returns(Task.FromResult(toAdd));

            var codingSkillService = new CodingSkillService(_repository.Object);

            await Assert.ThrowsAsync<BadRequestException>(() => codingSkillService.Add(toAdd));
        }
    }
}
