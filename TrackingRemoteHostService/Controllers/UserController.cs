using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TrackingRemoteHostService.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями системы
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields
        private readonly ILogger<UserController> _logger;
        #endregion

        #region Constructor
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        #endregion

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int userId)
        {
            return Ok();
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult AddUser([FromBody] string value)
        {
            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {

        }
    }
}
