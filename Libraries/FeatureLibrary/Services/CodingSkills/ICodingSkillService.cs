using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Models;

namespace FeatureLibrary.Services
{
    public interface ICodingSkillService
    {
        Task<IEnumerable<CodingSkillEntity>> GetByFilter(CodingSkillFilter filter);
        Task<CodingSkillEntity> GetById(long id);
        Task<CodingSkillEntity> Add(CodingSkillEntity newSkill, long? userId);
        Task<CodingSkillEntity> Update(long id, CodingSkillEntity modifiedSkill);
        Task Delete(long id);
        Task<IEnumerable<CodingSkillEntity>> GetByUserId(long userId);
    }
}
