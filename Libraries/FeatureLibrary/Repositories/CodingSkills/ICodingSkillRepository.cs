using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Models;
using System.Linq;

namespace FeatureLibrary.Repositories
{
    public interface ICodingSkillRepository
    {
        Task<IEnumerable<CodingSkillEntity>> GetByFilter(CodingSkillFilter filter);
        Task<CodingSkillEntity> GetById(long id);
        Task<CodingSkillEntity> Add(CodingSkillEntity newSkill);
        Task Update(CodingSkillEntity updatedSkill);
        Task Delete(CodingSkillEntity deletedSkill);
        Task<IEnumerable<CodingSkillEntity>> GetByUserId(long userId);
        Task<IEnumerable<CodingSkillEntity>> GetAll();
        Task<ILookup<long, CodingSkillEntity>> GetByUserIds(IEnumerable<long> userIds);
    }
}
