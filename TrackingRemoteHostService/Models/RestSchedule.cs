namespace TrackingRemoteHostService.Models
{
    /// <summary>
    /// Модель для rest добавления хоста с интервалом запроса
    /// </summary>
    public class RestSchedule
    {
        /// <summary>
        /// Хост который проверяется на доступность
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Интервал запроса в секундах
        /// </summary>
        public int Interval { get; set; }
    }
}
