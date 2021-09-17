using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrackingRemoteHostService.Models
{
    /// <summary>
    /// Расписания пользователей
    /// </summary>
    [Table("userschedule", Schema = "public")]
    public class UserSchedule : BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Column("userid"), Required]
        public int UserId { get; set; }
        /// <summary>
        /// Навигационное свой
        /// </summary>
        [JsonIgnore]
        public User User { get; set; }
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Column("scheduleId"), Required]
        public int ScheduleId { get; set; }
        /// <summary>
        /// Навигационное свой
        /// </summary>
        [JsonIgnore]
        public Schedule Schedule { get; set; }
    }
}
