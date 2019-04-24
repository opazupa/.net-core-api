using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Database;
using FeatureLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FeatureLibrary.Repositories
{
    /// <summary>
    /// Coding skill repository.
    /// </summary>
    public class CodingSkillRepository : ICodingSkillRepository
    {
        private readonly FeatureContext _context;

        public CodingSkillRepository(FeatureContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add the specified newSkill.
        /// </summary>
        /// <returns></returns>
        /// <param name="newSkill">New skill</param>
        public async Task Add(CodingSkill newSkill)
        {
            await _context.CodingSkills.AddAsync(newSkill);
        }

        /// <summary>
        /// Delete the specified deletedSkill.
        /// </summary>
        /// <param name="deletedSkill">Deleted skill</param>
        public void Delete(CodingSkill deletedSkill)
        {
            _context.CodingSkills.Remove(deletedSkill);
        }

        /// <summary>
        /// Get the skills by filter.
        /// </summary>
        /// <returns>List of skills matching given filter.</returns>
        /// <param name="filter">Filter</param>
        public async Task<IEnumerable<CodingSkill>> GetByFilter(CodingSkillFilter filter)
        {
            var codingSkills = _context.CodingSkills.AsQueryable();

            // Apply filter
            if (filter.Levels != null && filter.Levels.Any())
            {
                codingSkills = codingSkills.Where(skill => filter.Levels.Contains(skill.Level));
            }
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                codingSkills = codingSkills.Where(skill => skill.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));
            }

            return await codingSkills.OrderBy(skill => skill.Level).ToListAsync();
        }

        /// <summary>
        /// Get the coding skill by id.
        /// </summary>
        /// <returns>Found skill</returns>
        /// <param name="id">Id/param>
        public async Task<CodingSkill> GetById(long id)
        {
            return await _context.CodingSkills.Where(skill => skill.Id == id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Update the specified copding skill.
        /// </summary>
        /// <param name="updatedSkill">Updated skill</param>
        public void Update(CodingSkill updatedSkill)
        {
            _context.CodingSkills.Update(updatedSkill);
        }
    }
}
