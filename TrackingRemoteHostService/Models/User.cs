using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrackingRemoteHostService.Models
{
    /// <summary>
    /// Модель данных сущности пользователь
    /// </summary>
    [Table("users", Schema = "public")]
    public class User: BaseModel
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Column("firstname"), Required]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Column("secondname"), Required]
        public string SecondName { get; set; }
        /// <summary>
        /// Навигационное свойство
        /// </summary>
        [JsonIgnore]
        public virtual AuthUser Auth { get; set; }
    }
}
