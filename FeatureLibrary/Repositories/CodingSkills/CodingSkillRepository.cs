using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Database;
using FeatureLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FeatureLibrary.Repositories
{
    public class CodingSkillRepository : ICodingSkillRepository
    {
        private readonly FeatureContext _context;

        public CodingSkillRepository(FeatureContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<CodingSkill>> GetByFilter(CodingSkillFilter filter)
        {
            return await _context.CodingSkills.ToListAsync();
        }
    }
}
