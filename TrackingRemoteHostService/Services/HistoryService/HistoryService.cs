using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingRemoteHostService.Extensions;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.DbService;
using TrackingRemoteHostService.Services.PingService;
using Microsoft.Extensions.DependencyInjection;

namespace TrackingRemoteHostService.Services.HistoryService
{
    class HistoryService : IHistoryService
    {
        #region Fields
        private readonly ILogger<HistoryService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPingService _pingService;
        #endregion

        #region Constructor
        public HistoryService(ILogger<HistoryService> logger,
                              IServiceProvider serviceProvider,
                              IPingService pingService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _pingService = pingService;
        }
        #endregion

        #region IHistoryService
        public IEnumerable<HostStatus> GetHistories(int userId, long startTime, long endTime)
        {
            try
            {
                _logger.LogInformation($"Get history {startTime}-{endTime}");

                using (var efCore = _serviceProvider.GetService<EfCoreService>())
                {
                    var userSchedules = efCore.UserSchedules.Where(w => w.UserId == userId);
                    var normalStartTime = startTime.GetNormalTime();
                    var normalEndTime = endTime.GetNormalTime();
                    return efCore.Histories.Where(w => userSchedules
                                                               .Any(w => w.ScheduleId == w.ScheduleId) && w.NormalTime >= normalEndTime && w.NormalTime <= normalEndTime)
                                                               .Include(w => w.Schedule)
                                                               .Include(w => w.Schedule.Host)
                                                               .Select(w => new HostStatus { Date = w.NormalTime, Available = w.Available, Host = w.Schedule.Host.Url });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(GetHistories)}");
                return new List<HostStatus>();
            }
        }

        public async Task<IEnumerable<HostStatus>> GetCurrentStatus(int userId)
        {
            var efCore = _serviceProvider.GetService<EfCoreService>();
            try
            {
                var userSchedules = efCore.UserSchedules.Where(w => w.UserId == userId).Include(w => w.Schedule).Include(w => w.Schedule.Host).ToList();

                var hostStatuses = new List<HostStatus>();

                foreach (var schedule in userSchedules)
                {
                    var status = await _pingService.PingHost(schedule.Schedule.Host.Url);
                    var hostStatus = new HostStatus
                    {
                        Host = schedule.Schedule.Host.Url,
                        Date = DateTime.Now,
                        Available = status
                    };
                    hostStatuses.Add(hostStatus);
                }

                return hostStatuses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(GetHistories)}");
                return new List<HostStatus>();
            }
            finally
            {
                efCore.Dispose();
            }
        }

        public Task AddHistory(int scheduleId, bool available)
        {
            try
            {
                _logger.LogInformation($"add new history {scheduleId}");

                var history = new History
                {
                    ScheduleId = scheduleId,
                    Available = available,
                    Date = BitConverter.GetBytes(DateTime.Now.Ticks)
                };


                using (var efCore = _serviceProvider.GetService<EfCoreService>())
                {
                    efCore.Histories.Add(history);
                    efCore.SaveChanges();
                }
                _logger.LogDebug($"Added new history {history.Id}");

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(AddHistory)}");
                return Task.CompletedTask;
            }
        }
        #endregion
    }
}
