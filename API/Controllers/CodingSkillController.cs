using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using FeatureLibrary.Models;
using API.FilterModels;

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
        /// <response code="200">Found coding skills.</response>
        /// <response code="204">If no skills are found.</response> 
        [HttpGet]
        public ActionResult<IEnumerable<CodingSkill>> Get([FromQuery] CodingSkillFilter filter)
        {
            return new CodingSkill[] { };
        }

        /// <summary>
        /// Get coding skill by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Found coding skill.</returns>
        /// <response code="200">Found coding skill with the id.</response>
        /// <response code="404">If no skills are found with the id.</response> 
        [HttpGet("{id}")]
        public ActionResult<CodingSkill> Get(int id)
        {
            return new CodingSkill();
        }

        /// <summary>
        /// Post new coding skill.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /coding-skill
        ///     {
        ///        "id": 1,
        ///        "name": "ASP.NET Core",
        ///        "level": 3
        ///     }
        ///
        /// </remarks>
        /// <param name="skill"></param>
        /// <returns>A newly created coding skill item</returns>
        /// <response code="201">Returns the newly created coding skill</response>
        /// <response code="400">If the item is null or invalid.</response> 
        [HttpPost]
        public void Post([FromBody] CodingSkill skill)
        {
        }

        /// <summary>
        /// Update the given coding skill.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /coding-skill/1
        ///     {
        ///        "name": "ASP.NET Core",
        ///        "level": 5
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="skill"></param>
        /// <returns> The updated coding skill item</returns>
        /// <response code="200">Returns the updated created coding skill</response>
        /// <response code="400">If the item is null or invalid.</response> 
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CodingSkill skill)
        {
        }

        /// <summary>
        /// Delete the coding skill.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Coding skill deleted</response>
        /// <response code="404">If the coding skill is not found.</response> 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
