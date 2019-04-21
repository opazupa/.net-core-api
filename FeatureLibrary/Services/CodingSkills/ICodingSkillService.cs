using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Models;

namespace FeatureLibrary.Services
{
    public interface ICodingSkillService
    {
        Task<IEnumerable<CodingSkill>> GetByFilter(CodingSkillFilter filter);
    }
}
