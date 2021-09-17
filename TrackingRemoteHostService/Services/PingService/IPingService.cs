using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;

namespace TrackingRemoteHostService.Services.PingService
{
    /// <summary>
    /// Сервис для пинга хоста
    /// </summary>
    interface IPingService
    {
        /// <summary>
        /// Пинг
        /// </summary>
        /// <param name="host">Хост</param>
        /// <returns></returns>
        Task<bool> PingHost(string host);
    }
}
