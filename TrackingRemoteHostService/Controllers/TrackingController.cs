using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.HistoryService;
using TrackingRemoteHostService.Services.HostsService;
using TrackingRemoteHostService.Services.ScheduleService;
using TrackingRemoteHostService.Services.UserScheduleService;

namespace TrackingRemoteHostService.Controllers
{
    /// <summary>
    /// Контроллер для управления над хостами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class TrackingController : ControllerBase
    {
        #region Fields
        private readonly ILogger<TrackingController> _logger;
        private readonly IHostsService _hostsService;
        private readonly IScheduleService _scheduleService;
        private readonly IUserScheduleService _userScheduleService;
        private readonly IHistoryService _historyService;
        #endregion

        #region Constructor
        public TrackingController(ILogger<TrackingController> logger,
                                  IHostsService hostsService,
                                  IScheduleService scheduleService,
                                  IUserScheduleService userScheduleService,
                                  IHistoryService historyService)
        {
            _logger = logger;
            _hostsService = hostsService;
            _scheduleService = scheduleService;
            _userScheduleService = userScheduleService;
            _historyService = historyService;
        }
        #endregion

        #region Actions
        /// <summary>
        /// Добавление хоста для проверки доступности
        /// Авторизованный пользователь
        /// </summary>
        /// <param name="host">Хост</param>
        /// <returns>Идентификатор</returns>
        [HttpPost]
        public async Task<ActionResult<int>> AddHost([FromBody] RestSchedule host)
        {
            try
            {
                _logger.LogInformation($"POST api/tracking");

                if (host == null)
                {
                    return BadRequest("Объект пуст!");
                }

                var errors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(host.Host))
                {
                    errors.AppendLine($"Хост не может быть пустым");
                }
                if (host.Interval <= 0)
                {
                    errors.AppendLine($"Интервал не может быть равно нулю или меньше нуля");
                }

                if (errors.Length > 0)
                {
                    return BadRequest(errors.ToString());
                }

                var hostId = await _hostsService.AddHost(host.Host);
                var scheduleId = await _scheduleService.AddShedule(hostId.Value, host.Interval);
                var userId = System.Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var userScheduleId = await _userScheduleService.AddUserShedule(userId, scheduleId.Value);

                return new OkObjectResult(userScheduleId);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получение текущего состояния работоспособности проверяемых адресов
        /// Авторизованный пользователь
        /// </summary>
        /// <returns></returns>
        [HttpGet("history/current")]
        public async Task<ActionResult<List<HostStatus>>> GetCurrentStateHostsAvailability()
        {
            try
            {
                _logger.LogInformation("GET api/tracking/history/current");
                var userId = System.Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await _historyService.GetCurrentStatus(userId);
                return new OkObjectResult(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Получение истории проверок за заданный промежуток времени
        /// Авторизованный пользователь
        /// </summary>
        /// <param name="startTime">Дата начала</param>
        /// <param name="endTime">Дата окончания</param>
        /// <returns></returns>
        [HttpGet("history")]
        public ActionResult<IEnumerable<HostStatus>> GetAvailabilityHostInterval(long startTime, long endTime)
        {
            try
            {
                var errors = new StringBuilder();
                if (startTime <= 0)
                {
                    errors.AppendLine($"Дата начала не должно быть null");
                }
                if (endTime <= 0)
                {
                    errors.AppendLine($"Дата окончания не должно быть null");
                }
                if (endTime <= startTime)
                {
                    errors.AppendLine($"Дата окончания не должно быть меньше или равно дате начала");
                }

                if (errors.Length > 0)
                {
                    return BadRequest(errors.ToString());
                }


                _logger.LogInformation($"GET api/tracking/history?startTime={startTime}&endTime={endTime}");
                var userId = System.Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = _historyService.GetHistories(userId, startTime, endTime);
                return new OkObjectResult(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
