using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Models;
using FeatureLibrary.Repositories;

namespace FeatureLibrary.Services
{
    public class CodingSkillService: ICodingSkillService
    {
        private readonly ICodingSkillRepository _codingSkillRepository;
        public CodingSkillService(ICodingSkillRepository codingSkillRepository)
        {
            _codingSkillRepository = codingSkillRepository;
        }

        public async Task<IEnumerable<CodingSkill>> GetByFilter(CodingSkillFilter filter)
        {
            return await _codingSkillRepository.GetByFilter(filter);
        }
    }
}
