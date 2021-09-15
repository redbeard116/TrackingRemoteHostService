using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrackingRemoteHostService.Models
{
    /// <summary>
    /// Модель данных сущности хосты
    /// </summary>
    [Table("hosts", Schema = "public")]
    class Host : BaseModel
    {
        /// <summary>
        /// Ссылка на ност
        /// </summary>
        [Column("url"), Required]
        public string Url { get; set; }
        /// <summary>
        /// Навигационное свой
        /// </summary>
        [JsonIgnore]
        public List<Schedule> Shedules { get; set; }
    }
}
