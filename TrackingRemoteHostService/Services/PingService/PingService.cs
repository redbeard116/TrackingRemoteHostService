using Microsoft.Extensions.Logging;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace TrackingRemoteHostService.Services.PingService
{
    class PingService : IPingService
    {
        #region Fields
        private readonly ILogger<PingService> _logger;
        #endregion

        #region Constructor
        public PingService(ILogger<PingService> logger)
        {
            _logger = logger;
        }
        #endregion

        #region IPingService
        public async Task<bool> PingHost(string host)
        {
            try
            {
                var ping = new Ping();
                var reply = await ping.SendPingAsync(host);
                _logger.LogInformation($"Ping host = '{host}', status = '{reply.Status}'");
                return reply.Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in checked host '{host}'");
                throw;
            }
        }
        #endregion    
    }
}
