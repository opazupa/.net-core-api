using System.Collections.Generic;
using System.Threading.Tasks;
using FeatureLibrary.Models;
using FeatureLibrary.Services;
using CoreLibrary.Services.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Coding skill controller.
    /// </summary>
    [Route("api/coding-skill")]
    [ApiController]
    public class CodingSkillController : ControllerBase
    {
        private readonly ICodingSkillService _codingSkillService;
        private readonly IPersistenceService _dbTransaction;

        public CodingSkillController(ICodingSkillService codingSkillService, IPersistenceService persistenceService)
        {
            _codingSkillService = codingSkillService;
            _dbTransaction = persistenceService;
        }

        /// <summary>
        /// Get coding skills by filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Coding skills matching to filter values.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CodingSkill>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] CodingSkillFilter filter)
        {
            var skills =  await _codingSkillService.GetByFilter(filter);
            return Ok(skills);
        }

        /// <summary>
        /// Get coding skill by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Found coding skill.</returns>
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(typeof(CodingSkill), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var skill = await _codingSkillService.GetById(id);
            return Ok(skill);
        }

        /// <summary>
        /// Post new coding skill.
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>The newly created coding skill item</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CodingSkill), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] CodingSkill skill)
        {
            long createdSkill = await _codingSkillService.Add(skill);
            await _dbTransaction.CompleteAsync();

            return CreatedAtRoute("GetById", new { Id = createdSkill }, skill);
        }

        /// <summary>
        /// Update the given coding skill.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="skill"></param>
        /// <returns> The updated coding skill item</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CodingSkill), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(long id, [FromBody] CodingSkill skill)
        {
            await _codingSkillService.Update(id, skill);
            await _dbTransaction.CompleteAsync();

            var updatedSkill = await _codingSkillService.GetById(id);
            return Ok(updatedSkill);
        }

        /// <summary>
        /// Delete the coding skill.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _codingSkillService.Delete(id);
            await _dbTransaction.CompleteAsync();
            return Ok();
        }
    }
}
