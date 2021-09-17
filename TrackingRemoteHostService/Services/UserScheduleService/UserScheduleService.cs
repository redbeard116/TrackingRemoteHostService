using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.DbService;

namespace TrackingRemoteHostService.Services.UserScheduleService
{
    class UserScheduleService : IUserScheduleService
    {
        #region MyRegion
        private readonly ILogger<UserScheduleService> _logger;
        private readonly EfCoreService _efCoreService;
        #endregion

        #region Constructor
        public UserScheduleService(ILogger<UserScheduleService> logger,
                                   EfCoreService efCoreService)
        {
            _logger = logger;
            _efCoreService = efCoreService;
        }
        #endregion

        #region IUserScheduleService
        public async Task<int?> AddUserShedule(int userId, int scheduleId)
        {
            try
            {
                _logger.LogInformation($"Add user schedule {userId}, {scheduleId}");

                var userScheduleId = CheckUserSchedule(userId, scheduleId);

                if (userScheduleId.HasValue)
                {
                    return userScheduleId;
                }

                var userSchedule = new UserSchedule
                {
                    UserId = userId,
                    ScheduleId = scheduleId
                };

                await _efCoreService.UserSchedules.AddAsync(userSchedule);
                await _efCoreService.SaveChangesAsync();

                _logger.LogDebug($"User schedule created {userSchedule.Id}");

                return userSchedule.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(AddUserShedule)}");
                throw;
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Проверка существования в таблице бд
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="scheduleId">Идентификатор расписания</param>
        /// <returns></returns>
        private int? CheckUserSchedule(int userId, int scheduleId)
        {
            return _efCoreService.UserSchedules.FirstOrDefault(w => w.UserId == userId && w.ScheduleId == scheduleId)?.Id;
        }
        #endregion
    }
}