using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;
using TrackingRemoteHostService.Services.DbService;

namespace TrackingRemoteHostService.Services.HostsService
{
    class HostsService : IHostsService
    {
        #region Fields
        private readonly ILogger<HostsService> _logger;
        private readonly EfCoreService _efCoreService;
        #endregion

        #region Constructor
        public HostsService(ILogger<HostsService> logger,
                            EfCoreService efCoreService)
        {
            _logger = logger;
            _efCoreService = efCoreService;
        }
        #endregion

        #region IHostsService
        public async Task<int?> AddHost(string url)
        {
            try
            {
                _logger.LogInformation($"Add new host {url}");

                var validUrl = GetValidUrl(url);
                var hostId = CheckUrl(validUrl);

                if (hostId.HasValue)
                {
                    return hostId;
                }

                var host = new Host
                {
                    Url = validUrl
                };

                await _efCoreService.Hosts.AddAsync(host);
                await _efCoreService.SaveChangesAsync();

                _logger.LogInformation($"Added new host {host.Url}");

                return host.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(AddHost)}");
                throw;
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Проверка хоста в базе
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private int? CheckUrl(string url)
        {
            return _efCoreService.Hosts.FirstOrDefault(w => w.Url.Contains(url))?.Id;
        }
        /// <summary>
        /// Получение валидной ссылки
        /// </summary>
        /// <param name="url">Ссылка</param>
        /// <returns>Валидная ссылка</returns>
        private string GetValidUrl(string url)
        {
            var validUrl = string.Empty;
            if (!(url.ToLower().StartsWith("http://") || url.ToLower().StartsWith("https://")))
            {
                validUrl = "http://" + url.ToLower();
            }
            if (!url.EndsWith("/"))
            {
                validUrl += "/";
            }

            return validUrl;
        }
        #endregion
    }
}
