using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Models;

namespace FeatureLibrary.Repositories
{
    public interface ICodingSkillRepository
    {
        Task<IEnumerable<CodingSkill>> GetByFilter(CodingSkillFilter filter);
        Task<CodingSkill> GetById(long id);
        Task Add(CodingSkill newSkill);
        void Update(CodingSkill updatedSkill);
        void Delete(CodingSkill deletedSkill);
    }
}
