using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureLibrary.Models;
using FeatureLibrary.Models.Entities;
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
        public async Task<CodingSkillEntity> Add(CodingSkillEntity newSkill)
        {
            await _context.CodingSkills.AddAsync(newSkill);
            return newSkill;
        }

        /// <summary>
        /// Delete the specified deletedSkill.
        /// </summary>
        /// <param name="deletedSkill">Deleted skill</param>
        public Task Delete(CodingSkillEntity deletedSkill)
        {
            return Task.FromResult(_context.CodingSkills.Remove(deletedSkill));
        }

        /// <summary>
        /// Get the skills by filter.
        /// </summary>
        /// <returns>List of skills matching given filter.</returns>
        /// <param name="filter">Filter</param>
        public async Task<IEnumerable<CodingSkillEntity>> GetByFilter(CodingSkillFilter filter)
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
        /// Get all skills as IQueryable
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CodingSkillEntity>> GetAll()
        {
            return await _context.CodingSkills.ToListAsync();
        }

        /// <summary>
        /// Get the coding skill by id.
        /// </summary>
        /// <returns>Found skill</returns>
        /// <param name="id"></param>
        public async Task<CodingSkillEntity> GetById(long id)
        {
            return await _context.CodingSkills
                .Where(skill => skill.Id == id)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Get the coding skills by user id.
        /// </summary>
        /// <returns>Found skill</returns>
        /// <param name="userId"></param>
        public async Task<IEnumerable<CodingSkillEntity>> GetByUserId(long userId)
        {
            return await _context.CodingSkills
                .Where(skill => skill.UserId == userId)
                .ToListAsync();
        }

        /// <summary>
        /// Get coding skills by user ids
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns>Lookup of coding skills for users.</returns>
        public Task<ILookup<long, CodingSkillEntity>> GetByUserIds(IEnumerable<long> userIds)
        {
            return Task.FromResult(_context.CodingSkills.ToLookup(s => s.UserId));
        }

        /// <summary>
        /// Update the specified coding skill.
        /// </summary>
        /// <param name="updatedSkill">Updated skill</param>
        public Task Update(CodingSkillEntity updatedSkill)
        {
            return Task.FromResult(_context.CodingSkills.Update(updatedSkill));
        }
    }
}
