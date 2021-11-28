using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Landing.Server.Auth
{
    /// <summary>
    /// Параметры формирования токена
    /// </summary>
    public class AuthOptions
    {
        public const string Issuer = "authServer";
        public const string Audience = "resourceServer";
        public const string Secret = "secret123456789+";
        public const int TokenLifetime = 3600;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
    /// <summary>
    /// Роли пользователей
    /// </summary>
    enum LRols
    {
        Admin,
        Guest
    }
}