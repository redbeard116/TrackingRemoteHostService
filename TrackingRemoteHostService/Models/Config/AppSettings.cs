namespace TrackingRemoteHostService.Models.Config
{
    /// <summary>
    /// Настройки системы
    /// </summary>
    class AppSettings
    {
        /// <summary>
        /// Данные для генерации токена авторизации
        /// </summary>
        public AuthOptions AuthOptions { get; set; }
    }
}
