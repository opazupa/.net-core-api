using System.Threading.Tasks;
using CoreLibrary.Services.Persistence;
using FeatureLibrary.Models;
using FeatureLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Auth controller
    /// </summary>
    [Route("api/auth")]
    [Produces("application/json")]
    [AllowAnonymous]
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
        /// Login
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] Authentication auth)
        {
            var authResult = await _userService.Authenticate(auth);
            return Ok(authResult);
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] Authentication auth)
        {
            var userId = await _userService.CreateUser(auth);
            await _dbTransaction.CompleteAsync();

            return StatusCode(StatusCodes.Status201Created, userId);
        }
    }
}
