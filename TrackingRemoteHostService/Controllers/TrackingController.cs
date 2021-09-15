using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace TrackingRemoteHostService.Controllers
{
    /// <summary>
    /// Контроллер для управления над хостами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        #region Fields
        private readonly ILogger<TrackingController> _logger;
        #endregion

        #region Constructor
        public TrackingController(ILogger<TrackingController> logger)
        {
            _logger = logger;
        }
        #endregion

        // GET: api/<TrackingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TrackingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TrackingController>
        [HttpPost]
        public void AddHost([FromBody] string value)
        {
        }

        // DELETE api/<TrackingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
