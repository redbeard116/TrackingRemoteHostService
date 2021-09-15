using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.AuthService;

namespace TrackingRemoteHostService.Controllers
{
    /// <summary>
    /// Контроллер для авторизации в системе
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        #region Fields
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        #endregion

        #region Constructor
        public AuthController(ILogger<AuthController> logger,
                              IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Авторизация пользователя в системе
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Токен доступа и данные пользователя</returns>
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<ActionResult<Responce>> Auth([FromBody] UserAuth user)
        {
            try
            {
                _logger.LogInformation("POST api/auth/signin");
                var result = await _authService.AuthUser(user);
                return new OkObjectResult(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public void LogOut()
        {

        }
        #endregion
    }
}
