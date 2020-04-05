using System.Threading.Tasks;
using CoreLibrary.Services.Persistence;
using FeatureLibrary.Models;
using FeatureLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    /// <summary>
    /// Auth controller
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPersistenceService _dbTransaction;

        public AuthController(IUserService userService, IPersistenceService persistenceService)
        {
            _userService = userService;
            _dbTransaction = persistenceService;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        public async Task<IActionResult> Get([FromBody] Authentication auth)
        {
            var userId = await _userService.CreateUser(auth);
            return StatusCode(StatusCodes.Status201Created, userId);
        }
    }
}
