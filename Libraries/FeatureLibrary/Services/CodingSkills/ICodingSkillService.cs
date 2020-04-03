using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Models;

namespace FeatureLibrary.Services
{
    public interface ICodingSkillService
    {
        Task<IEnumerable<CodingSkill>> GetByFilter(CodingSkillFilter filter);
        Task<CodingSkill> GetById(long id);
        Task<long> Add(CodingSkill newSkill);
        Task Update(long id, CodingSkill updatedSkill);
        Task Delete(long id);

    }
}
