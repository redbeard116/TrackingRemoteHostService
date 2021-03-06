using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingRemoteHostService.Models
{
    /// <summary>
    /// История доступности хостов
    /// </summary>
    [Table("history", Schema = "public")]
    public class History : BaseModel
    {
        /// <summary>
        /// Расписание
        /// </summary>
        [Column("scheduleid"), Required]
        public int ScheduleId { get; set; }
        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public Schedule Schedule { get; set; }
        /// <summary>
        /// Дата запрашивания
        /// </summary>
        [Column("date"), Required]
        [Timestamp]
        public DateTime Date { get; set; }
        /// <summary>
        /// Доступен
        /// </summary>
        [Column("available"), Required]
        public bool Available { get; set; }
    }
}