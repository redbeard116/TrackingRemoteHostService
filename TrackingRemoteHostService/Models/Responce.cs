using System.Text.Json.Serialization;

namespace TrackingRemoteHostService.Models
{
    /// <summary>
    /// Возвращаемый результата при успешной авторизации
    /// </summary>
    public class Responce
    {
        /// <summary>
        /// Токен доступа
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [JsonPropertyName("username")]
        public string UserName { get; set; }
        /// <summary>
        /// Идектификатор пользователя
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
