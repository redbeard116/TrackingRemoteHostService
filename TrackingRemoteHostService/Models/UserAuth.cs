namespace TrackingRemoteHostService.Models
{
    /// <summary>
    /// Модель для авторизации в системе
    /// </summary>
    public class UserAuth
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
