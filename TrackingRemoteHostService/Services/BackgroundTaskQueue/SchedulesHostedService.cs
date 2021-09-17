using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.DbService;
using TrackingRemoteHostService.Services.HistoryService;
using TrackingRemoteHostService.Services.PingService;

namespace TrackingRemoteHostService.Services.BackgroundTaskQueue
{
    class SchedulesHostedService : IHostedService
    {
        private readonly ILogger<SchedulesHostedService> _logger;
        private readonly EfCoreService _efCoreService;
        private readonly IPingService _pingService;
        private readonly IHistoryService _historyService;
        private readonly List<Schedule> _runnedSchedules;

        private CancellationToken _cancellationToken;
        private Thread _thread;
        private IEnumerable<Schedule> _schedules;

        public SchedulesHostedService(ILogger<SchedulesHostedService> logger, IPingService pingService, EfCoreService efCoreService, IHistoryService historyService)
        {
            _logger = logger;
            _pingService = pingService;
            _efCoreService = efCoreService;
            _historyService = historyService;
            _runnedSchedules = new List<Schedule>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Queued Background Task Schedule is starting");
            _cancellationToken = cancellationToken;
            _runnedSchedules.Clear();
            _schedules = _efCoreService.Schedules.Include(w => w.Host).ToList();
            _efCoreService.SavingChanges += _efCoreService_SavingChanges;
            _thread = new Thread(StartPingHosts);
            _thread.Start();
            _logger.LogInformation($"Queued Background Task Schedule is complete");
            return Task.CompletedTask;
        }

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


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _efCoreService.SavingChanges -= _efCoreService_SavingChanges;
            cancellationToken.ThrowIfCancellationRequested();
            return Task.CompletedTask;
        }

        private void _efCoreService_SavingChanges(object sender, SavingChangesEventArgs e)
        {
            _schedules = _efCoreService.Schedules.Include(w => w.Host).ToList();
        }
    }
}
