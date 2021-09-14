using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingRemoteHostService.Models
{
    /// <summary>
    /// Авторизационные данные сущности пользователь
    /// </summary>
    [Table("auths", Schema = "public")]
    public class AuthUser : BaseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Column("userid"), Required]
        public int UserId { get; set; }
        /// <summary>
        /// Навигационное свой
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        [Column("login"), Required]
        public string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        [Column("password"), Required]
        public string Password { get; set; }
    }
}
