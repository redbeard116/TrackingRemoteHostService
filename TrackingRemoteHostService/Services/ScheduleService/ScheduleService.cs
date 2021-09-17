﻿using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.DbService;

namespace TrackingRemoteHostService.Services.ScheduleService
{
    class ScheduleService : IScheduleService
    {
        #region Fields
        private readonly ILogger<ScheduleService> _logger;
        private readonly EfCoreService _efCoreService;
        #endregion

        #region Constructor
        public ScheduleService(ILogger<ScheduleService> logger,
                              EfCoreService efCoreService)
        {
            _logger = logger;
            _efCoreService = efCoreService;

        }

        #endregion

        #region ISheduleService
        public async Task<int?> AddShedule(int hostId, int interval)
        {
            try
            {
                _logger.LogInformation($"Add new schedule {hostId}-{interval}");

                var sheduleId = ChenckShedule(hostId, interval);
                if (sheduleId.HasValue)
                {
                    return sheduleId;
                }

                var schedule = new Schedule
                {
                    HostId = hostId,
                    Interval = interval
                };

                await _efCoreService.Schedules.AddAsync(schedule);
                await _efCoreService.SaveChangesAsync();

                _logger.LogDebug($"Added schedule {schedule.Id}");

                return schedule.Id;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(AddShedule)}");
                throw;
            }
        }
        #endregion

        #region Private methods
        private int? ChenckShedule(int hostId, int interval)
        {
            return _efCoreService.Schedules.FirstOrDefault(w => w.HostId == hostId && w.Interval == interval)?.Id;
        }
        #endregion
    }
}
