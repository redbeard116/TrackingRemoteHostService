using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingRemoteHostService.Models;

namespace TrackingRemoteHostService.Services.ScheduleService
{
    /// <summary>
    /// Добавление частоты проверки
    /// </summary>
    public interface IScheduleService
    {
        /// <summary>
        /// Добавление расписания
        /// </summary>
        /// <param name="hostId">Идентификатор хоста</param>
        /// <param name="interval">Интервал между запусками</param>
        /// <returns></returns>
        Task<int?> AddShedule(int hostId, int interval);
        /// <summary>
        /// Получение всех расписании
        /// </summary>
        /// <returns></returns>
        IEnumerable<Schedule> GetAllSchedule();
        /// <summary>
        /// Событие добавления расписания
        /// </summary>
        event EventHandler<Schedule> AddSchedule;
    }
}
