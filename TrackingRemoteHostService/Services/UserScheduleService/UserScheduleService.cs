using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
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

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(AddUserShedule)}");
                throw;
            }
        }
        #endregion

        #region Private methods
        private int? CheckUserSchedule(int userId, int scheduleId)
        {
            return null;//_efCoreService.
        }
        #endregion
    }
}