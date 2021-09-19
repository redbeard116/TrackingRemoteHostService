using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.HistoryService;
using TrackingRemoteHostService.Services.PingService;
using TrackingRemoteHostService.Services.ScheduleService;

namespace TrackingRemoteHostService.Services.BackgroundTaskQueue
{
    class SchedulesHostedService : IHostedService
    {
        #region Fields
        private readonly ILogger<SchedulesHostedService> _logger;
        private readonly IPingService _pingService;
        private readonly IHistoryService _historyService;
        private readonly IScheduleService _scheduleService;
        private readonly List<Schedule> _runnedSchedules;

        private CancellationToken _cancellationToken;
        private Thread _thread;
        private List<Schedule> _schedules;
        #endregion

        #region Constructor
        public SchedulesHostedService(ILogger<SchedulesHostedService> logger, IPingService pingService, IHistoryService historyService, IScheduleService scheduleService)
        {
            _logger = logger;
            _pingService = pingService;
            _historyService = historyService;
            _scheduleService = scheduleService;
            _runnedSchedules = new List<Schedule>();
        } 
        #endregion

        #region IHostedService
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Queued Background Task Schedule is starting");
            _cancellationToken = cancellationToken;
            _runnedSchedules.Clear();
            _schedules = _scheduleService.GetAllSchedule().ToList();
            _scheduleService.AddSchedule += _scheduleService_AddSchedule;
            _thread = new Thread(StartPingHosts);
            _thread.Start();
            _logger.LogInformation($"Queued Background Task Schedule is complete");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _scheduleService.AddSchedule -= _scheduleService_AddSchedule;
            cancellationToken.ThrowIfCancellationRequested();
            return Task.CompletedTask;
        }
        #endregion

        #region Private methods
        private void StartPingHosts()
        {
            try
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    Task.Run(async () => await BuildWorkItem(_cancellationToken));
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogDebug($"Operation cancelled");
            }
        }

        private async ValueTask BuildWorkItem(CancellationToken token)
        {
            try
            {
                var schedule = _schedules.FirstOrDefault(w => !_runnedSchedules.Any(s => s.Id == w.Id));
                if (schedule != null)
                {
                    _runnedSchedules.Add(schedule);

                    await Task.Run(() => PingHost(token, schedule));
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogDebug($"Operation cancelled");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(BuildWorkItem)}");
            }
        }

        private async Task PingHost(CancellationToken token, Schedule schedule)
        {
            _logger.LogInformation($"Run ping host. Schedule  = '{schedule.Id}'");
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(schedule.Interval * 1000);
                    var status = await _pingService.PingHost(schedule.Host.Url);
                    await _historyService.AddHistory(schedule.Id, status);
                }
            }
            catch (OperationCanceledException)
            {

            }

            _logger.LogInformation($"Run ping host complete. Schedule  = '{schedule.Id}'");
        }

        private void _scheduleService_AddSchedule(object sender, Schedule schedule)
        {
            if (!_schedules.Any(w => w.Id == schedule.Id))
            {
                _schedules.Add(schedule);
            }
        } 
        #endregion
    }
}
