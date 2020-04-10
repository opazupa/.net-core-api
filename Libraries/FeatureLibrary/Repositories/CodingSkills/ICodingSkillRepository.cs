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
        void Update(CodingSkillEntity updatedSkill);
        void Delete(CodingSkillEntity deletedSkill);
        Task<IEnumerable<CodingSkillEntity>> GetByUserId(long userId);
        IQueryable<CodingSkillEntity> GetAsQueryable();
    }
}
