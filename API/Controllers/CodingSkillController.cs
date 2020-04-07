using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;
using CoreLibrary.Services.Persistence;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Extensions;
using FeatureLibrary.Models;
using FeatureLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Coding skill controller.
    /// </summary>
    [Authorize]
    [Route("api/coding-skill")]
    [Produces("application/json")]
    [ApiController]
    public class CodingSkillController : ControllerBase
    {
        private readonly ICodingSkillService _codingSkillService;
        private readonly IPersistenceService _dbTransaction;
        private readonly IMapper _mapper;

        public CodingSkillController(ICodingSkillService codingSkillService, IPersistenceService persistenceService, IMapper mapper)
        {
            _codingSkillService = codingSkillService;
            _dbTransaction = persistenceService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get coding skills by filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Coding skills matching to filter values.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CodingSkill>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] CodingSkillFilter filter)
        {
            var entities = await _codingSkillService.GetByFilter(filter);
            return Ok(_mapper.Map<IEnumerable<CodingSkill>>(entities));
        }

        /// <summary>
        /// Get coding skill by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Found coding skill.</returns>
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(typeof(CodingSkill), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            var entity = await _codingSkillService.GetById(id);
            return Ok(_mapper.Map<CodingSkill>(entity));
        }

        /// <summary>
        /// Post new coding skill.
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>The newly created coding skill id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] NewSkill skill)
        {
            long createdSkillId = await _codingSkillService.Add(_mapper.Map<CodingSkillEntity>(skill), User.GetId());
            await _dbTransaction.CompleteAsync();

            return StatusCode(StatusCodes.Status201Created, createdSkillId);
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
        public async Task<ActionResult> Put(long id, [FromBody] ModifiedSkill skill)
        {
            await _codingSkillService.Update(id, _mapper.Map<CodingSkillEntity>(skill));
            await _dbTransaction.CompleteAsync();

            var updatedEntity = await _codingSkillService.GetById(id);
            return Ok(_mapper.Map<CodingSkill>(updatedEntity));

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
