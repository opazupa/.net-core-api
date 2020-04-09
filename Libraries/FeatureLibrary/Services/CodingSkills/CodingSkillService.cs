using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLibrary.Exceptions;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Models;
using FeatureLibrary.Repositories;
using System.Linq;
using System.Threading;

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
        /// <param name="userId"></param>
        public async Task<CodingSkillEntity> Add(CodingSkillEntity newSkill, long? userId)
        {
            if (string.IsNullOrWhiteSpace(newSkill.Name) || newSkill.Level == 0 || userId == null)
            {
                throw new BadRequestException($"Missing name {newSkill.Name} or level {newSkill.Level} for new skill.");
            }

            var skill = new CodingSkillEntity()
            {
                Name = newSkill.Name,
                Level = newSkill.Level,
                UserId = userId.Value
            };
            return await _codingSkillRepository.Add(skill);
        }

        /// <summary>
        /// Delete the coding skill with specified id.
        /// </summary>
        /// <returns></returns>
        /// <param name="id">Id</param>
        public async Task Delete(long id)
        {
            CodingSkillEntity deletedSkill = await _codingSkillRepository.GetById(id);

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
        public async Task<IEnumerable<CodingSkillEntity>> GetByFilter(CodingSkillFilter filter)
        {
            return await _codingSkillRepository.GetByFilter(filter);
        }

        /// <summary>
        /// Get skill by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CodingSkillEntity> GetById(long id)
        {
            CodingSkillEntity skill = await _codingSkillRepository.GetById(id);
            if (skill == null)
            {
                throw new NotFoundException($"Skill with id {id} not found.");
            }

            return skill;
        }

        /// <summary>
        /// Get skill by User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CodingSkillEntity>> GetByUserId(long userId)
        {
            return await _codingSkillRepository.GetByUserId(userId);
        }

        /// <summary>
        /// Get coding skills by user ids
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns>Lookup of coding skills for users.</returns>
        public async Task<ILookup<long, CodingSkillEntity>> GetSkillsByUserIds(IEnumerable<long> userIds)
        {
            // Get all by empty filter
            var skills = await _codingSkillRepository.GetByFilter(new CodingSkillFilter());
            return skills.ToLookup(s => s.Id, s => s);
        }

        /// <summary>
        /// Update the specified coding skill.
        /// </summary>
        /// <returns></returns>
        /// <param name="id">Id</param>
        /// <param name="modifiedSkill">Updated skill</param>
        public async Task<CodingSkillEntity> Update(long id, CodingSkillEntity modifiedSkill)
        {
            if (string.IsNullOrWhiteSpace(modifiedSkill.Name) || modifiedSkill.Level == 0)
            {
                throw new BadRequestException($"Missing name {modifiedSkill.Name} or level {modifiedSkill.Level} for updated skill.");
            }

            CodingSkillEntity skill = await _codingSkillRepository.GetById(id);
            if (skill == null)
            {
                throw new NotFoundException($"Skill with id {id} not found.");
            }

            skill.Level = modifiedSkill.Level;
            skill.Name = modifiedSkill.Name;
            _codingSkillRepository.Update(skill);
            
            return skill;
        }
    }
}
