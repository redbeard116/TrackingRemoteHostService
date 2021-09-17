using System.Threading.Tasks;

namespace TrackingRemoteHostService.Services.HostsService
{
    /// <summary>
    /// Интерфейс для работы с хостами
    /// </summary>
    public interface IHostsService
    {
        /// <summary>
        /// Добавление хоста
        /// </summary>
        /// <param name="url">Хост</param>
        /// <returns>Идентификатор</returns>
        Task<int?> AddHost(string url);
    }
}
