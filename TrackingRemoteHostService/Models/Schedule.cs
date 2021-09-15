using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrackingRemoteHostService.Models
{
    /// <summary>
    /// Расписания запусков
    /// </summary>
    [Table("shedules", Schema = "public")]
    class Schedule : BaseModel
    {
        /// <summary>
        /// Идентификатор хоста
        /// </summary>
        [Column("hostid"), Required]
        public int HostId { get; set; }
        /// <summary>
        /// Навигационное свой
        /// </summary>
        [JsonIgnore]
        public Host Host { get; set; }
        /// <summary>
        /// Интервал между запусками
        /// </summary>
        [Column("url"), Required]
        public int Interval { get; set; }
    }
}
