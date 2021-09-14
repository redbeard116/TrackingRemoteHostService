using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TrackingRemoteHostService.Models.Config
{
    /// <summary>
    /// Конфиг для генерации токена JWT
    /// </summary>
    class AuthOptions
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Потребитель токена
        /// </summary>
        public string Audience { get; set; } // потребитель токена
        /// <summary>
        /// Ключ для шифрации
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Время жизни токена (в минутах)
        /// </summary>
        public int Lifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
