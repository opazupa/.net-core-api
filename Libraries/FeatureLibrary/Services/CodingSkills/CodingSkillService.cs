using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLibrary.Exceptions;
using FeatureLibrary.Models;
using FeatureLibrary.Repositories;

namespace FeatureLibrary.Services
{
    /// <summary>
    /// Coding skill service.
    /// </summary>
    public class CodingSkillService : ICodingSkillService
    {
        private readonly ICodingSkillRepository _codingSkillRepository;
        public CodingSkillService(ICodingSkillRepository codingSkillRepository)
        {
            _codingSkillRepository = codingSkillRepository;
        }

        /// <summary>
        /// Add the specified coding skill.
        /// </summary>
        /// <returns>The added coding skill id</returns>
        /// <param name="newSkill">New skill</param>
        public async Task<long> Add(CodingSkill newSkill)
        {
            if (string.IsNullOrWhiteSpace(newSkill.Name) || newSkill.Level == 0)
            {
                throw new BadRequestException($"Missing name {newSkill.Name} or level {newSkill.Level} for new skill.");
            }
            await _codingSkillRepository.Add(newSkill);

            return newSkill.Id;
        }

        /// <summary>
        /// Delete the coding skill with specified id.
        /// </summary>
        /// <returns></returns>
        /// <param name="id">Id</param>
        public async Task Delete(long id)
        {
            CodingSkill deletedSkill = await _codingSkillRepository.GetById(id);

            if (deletedSkill == null)
            {
                throw new NotFoundException($"Skill with id {id} not found.");
            }

            _codingSkillRepository.Delete(deletedSkill);
        }

        /// <summary>
        /// Get coding skills by filter.
        /// </summary>
        /// <returns>List of coding skills matching to filter.</returns>
        /// <param name="filter">Filter</param>
        public async Task<IEnumerable<CodingSkill>> GetByFilter(CodingSkillFilter filter)
        {
            return await _codingSkillRepository.GetByFilter(filter);
        }

        public async Task<CodingSkill> GetById(long id)
        {
            CodingSkill skill = await _codingSkillRepository.GetById(id);
            if (skill == null)
            {
                throw new NotFoundException($"Skill with id {id} not found.");
            }

            return skill;
        }

        /// <summary>
        /// Update the specified coding skill.
        /// </summary>
        /// <returns></returns>
        /// <param name="id">Id</param>
        /// <param name="updatedSkill">Updated skill</param>
        public async Task Update(long id, CodingSkill updatedSkill)
        {
            if (string.IsNullOrWhiteSpace(updatedSkill.Name) || updatedSkill.Level == 0)
            {
                throw new BadRequestException($"Missing name {updatedSkill.Name} or level {updatedSkill.Level} for updated skill.");
            }

            CodingSkill skill = await _codingSkillRepository.GetById(id);
            if (skill == null)
            {
                throw new NotFoundException($"Skill with id {id} not found.");
            }

            skill.Level = updatedSkill.Level;
            skill.Name = updatedSkill.Name;
            _codingSkillRepository.Update(skill);
        }
    }
}
