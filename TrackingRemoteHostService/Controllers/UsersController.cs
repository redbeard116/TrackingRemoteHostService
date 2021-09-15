using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.UserService;

namespace TrackingRemoteHostService.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями системы
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Fields
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public UsersController(ILogger<UsersController> logger,
                               IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        #endregion

        #region Action
        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="user">Данные пользователя</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> AddUser([FromBody] CreateUser user)
        {
            try
            {
                _logger.LogDebug("POST api/users");
                var createdUser = await _userService.AddUser(user);
                return new OkObjectResult(createdUser);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
