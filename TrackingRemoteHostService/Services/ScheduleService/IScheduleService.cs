using System.Threading.Tasks;

namespace TrackingRemoteHostService.Services.ScheduleService
{
    /// <summary>
    /// Добавление частоты проверки
    /// </summary>
    interface IScheduleService
    {
        /// <summary>
        /// Добавление расписания
        /// </summary>
        /// <param name="hostId">Идентификатор хоста</param>
        /// <param name="interval">Интервал между запусками</param>
        /// <returns></returns>
        Task<int?> AddShedule(int hostId, int interval);
    }
}
