using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;

namespace TrackingRemoteHostService.Services.HistoryService
{
    /// <summary>
    /// Интерфейс для работы с историей
    /// </summary>
    public interface IHistoryService
    {
        /// <summary>
        /// Получение истории проверок за заданный промежуток времени 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        IEnumerable<HostStatus> GetHistories(int userId, long startTime, long endTime);
        /// <summary>
        /// Добавление истории доступности
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="available"></param>
        /// <returns></returns>
        Task AddHistory(int scheduleId, bool available);
        /// <summary>
        /// Получение текущего состояния работоспособности проверяемых адресов
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<HostStatus>> GetCurrentStatus(int userId);
    }
}
