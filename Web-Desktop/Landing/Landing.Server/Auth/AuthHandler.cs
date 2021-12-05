using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Landing.Library.ViewModel;
using System.Threading.Tasks;
using System.Security.Claims;
using Landing.Library.Model;
using System.Text;
using Landing.Server.Database.Query;
using Landing.Server.Data;
using Landing.Library.Logging;
using Landing.Library.Interfaces.Server;

namespace Landing.Server.Auth
{
    public class AuthHandler : IAccountHandler
    {
        public  ClaimsIdentity claimsNew;
        private readonly BaseAccount baseAccount;

        public AuthHandler()
        {
            this.baseAccount = new BaseAccount(Connection.GetConnectionString());

        }
        /// <summary>
        /// Авторизацция пользователя
        /// </summary>
        /// <param name="login">Модель данных для авторизации</param>
        /// <returns>модель UserResult если авторизация прошла успешно, в противном случае null</returns>
        public async Task<UserResult> Login(LoginViewModel login)
        {
            try
            {
                UserCheck result = await baseAccount.GetUserByEmailAndPassword(login.Email, login.Password);
                if (result.Succeeded)
                {
                    result.Token = TokenGenerate(result);
                    UserResult userResult = new UserResult
                    {
                        Name = result.Name,
                        Token = result.Token,
                        Role = result.Role,
                        Email = result.Email,
                        ImageId = result.ImageId
                    };
                    Log.Info($"Авторизация пользователя {userResult.Email}", "AuthHandler/Login");
                    return userResult;
                }
            }
            catch(Exception e)
            {
                Log.Error($"Ошибка авторизации пользователя {e.Message}", "AuthHandler/Login", e);
            }
            return null;
        }
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="register">Модель данных</param>
        /// <returns>модель UserResult если авторизация прошла успешно, в противном случае null</returns>
        public async Task<UserResult> Register(RegisterViewModel register)
        {
            User user = new User { Email = register.Email, UserName = register.Email };
            try
            {
                UserCheck result = await baseAccount.RegisterUser(register);
                if (result.Succeeded)
                {
                    result.Token = TokenGenerate(result);
                    UserResult userResult = new UserResult
                    {
                        Name = result.Name,
                        Token = result.Token,
                        Role = result.Role,
                        Email = result.Email,
                        ImageId = result.ImageId
                    };
                    Log.Info($"Регистрация пользователя {result.Email}", "AuthHandler/Register");
                    return userResult;
                }
            }
            catch(Exception e)
            {
                Log.Error($"Ошибка регистрации пользователя {e.Message}", "AuthHandler/Register", e);
                
            }
            return null;
        }
        /// <summary>
        /// Генератор токена
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Токен</returns>
        private static string TokenGenerate(UserCheck user)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(AuthOptions.Secret);
            if (user != null)
            {
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                        new Claim("role", user.Role),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(24),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

                SecurityToken token = jwtTokenHandler.CreateToken(tokenDescription);

                string jwtToken = jwtTokenHandler.WriteToken(token);
                return jwtToken;
            }
            return null;
        }
       
    }
}
