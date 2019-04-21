using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Models;

namespace FeatureLibrary.Repositories
{
    public interface ICodingSkillRepository
    {
        Task<IEnumerable<CodingSkill>> GetByFilter(CodingSkillFilter filter);
        //Task<CodingSkill> GetById(long id);
        //void Add(CodingSkill newSkill);
        //void Update(long id, CodingSkill updatedSkill);
        //void Delete(long id);
    }
}
