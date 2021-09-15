using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingRemoteHostService.Models
{
    /// <summary>
    /// Расписания пользователей
    /// </summary>
    [Table("userschedule", Schema = "public")]
    class UserSchedule : BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Навигационное свой
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// Навигационное свой
        /// </summary>
        public Schedule Schedule { get; set; }
    }
}
