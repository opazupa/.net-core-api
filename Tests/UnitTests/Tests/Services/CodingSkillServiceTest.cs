using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Exceptions;
using FeatureLibrary.Repositories;
using FeatureLibrary.Services;
using Moq;
using Xunit;
using static FeatureLibrary.Database.SkillMock;

namespace UnitTests.Services
{
    public class CodingSkillServiceTest
    {

        [Fact]
        public async Task AddCodingSkillWithNoLevel()
        {
            var toAdd = GetSkills(1).First();
            toAdd.Level = 0;

            var _repository = new Mock<ICodingSkillRepository>();
            _repository.Setup(x => x.Add(null)).Returns(Task.FromResult(toAdd));

            var codingSkillService = new CodingSkillService(_repository.Object);

            await Assert.ThrowsAsync<BadRequestException>(() => codingSkillService.Add(toAdd));
        }
    }
}
