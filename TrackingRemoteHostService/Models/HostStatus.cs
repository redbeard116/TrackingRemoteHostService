using System;

namespace TrackingRemoteHostService.Models
{
    /// <summary>
    /// Статус хоста
    /// </summary>
    public class HostStatus
    {
        /// <summary>
        /// Хост
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Доступность хоста
        /// </summary>
        public bool Available { get; set; }
    }
}
