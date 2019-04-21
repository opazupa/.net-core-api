using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using FeatureLibrary.Models;
using API.FilterModels;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    /// <summary>
    /// Coding skill controller.
    /// </summary>
    [Route("api/coding-skill")]
    [ApiController]
    public class CodingSkillController : ControllerBase
    {
        /// <summary>
        /// Get coding skills by filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Coding skills matching to filter values.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CodingSkill>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status204NoContent),]
        public ActionResult<IEnumerable<CodingSkill>> Get([FromQuery] CodingSkillFilter filter)
        {
            return new CodingSkill[] { };
        }

        /// <summary>
        /// Get coding skill by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Found coding skill.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CodingSkill), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public ActionResult<CodingSkill> Get(int id)
        {
            return new CodingSkill();
        }

        /// <summary>
        /// Post new coding skill.
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>The newly created coding skill item</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CodingSkill), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public void Post([FromBody] CodingSkill skill)
        {
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
        public void Put(int id, [FromBody] CodingSkill skill)
        {
        }

        /// <summary>
        /// Delete the coding skill.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public void Delete(int id)
        {
        }
    }
}
