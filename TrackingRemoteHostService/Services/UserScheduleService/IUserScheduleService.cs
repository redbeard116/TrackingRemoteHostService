using System.Threading.Tasks;

namespace TrackingRemoteHostService.Services.UserScheduleService
{
    /// <summary>
    /// Интерфейс для работы над пользовательскими расписаниями
    /// </summary>
    interface IUserScheduleService
    {
        /// <summary>
        /// Добавление расписания
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="scheduleId">Идентификатор расписания</param>
        /// <returns></returns>
        Task<int?> AddUserShedule(int userId, int scheduleId);
    }
}
