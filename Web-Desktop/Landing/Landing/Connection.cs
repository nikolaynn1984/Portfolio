using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Landing
{
    /// <summary>
    /// Соединение и авторизация пользователя
    /// </summary>
    public class Connection : IHostedService
    {
        /// <summary>
        /// Путь к API
        /// </summary>
        public static string Url { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public static string UserName { get; set;  }
        /// <summary>
        /// Пароль
        /// </summary>
        public static string Password {  get; set; }
        /// <summary>
        /// Токен
        /// </summary>
        public static string Token {  get; set;}

        public IConfiguration Configuration { get; }
        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="configuration">Конфигурация</param>
        public Connection(IConfiguration configuration)
        {
            this.Configuration = configuration;
            SetConnectionInfo();
        }
        /// <summary>
        /// Записываем параметры авторизации в поля
        /// </summary>
        private void SetConnectionInfo()
        {
            UserName = Configuration.GetSection("Agent").GetSection("username").Value;
            Password = Configuration.GetSection("Agent").GetSection("password").Value;
            Url = Configuration.GetSection("Agent").GetSection("url").Value;
        }
        /// <summary>
        /// Получаем токен для работы с API
        /// </summary>
        /// <param name="httpContext">Передаем HttpContext</param>
        /// <returns>Токен</returns>
        public static string GetToken(HttpContext httpContext)
        {
            var token = httpContext.Request.Cookies["token"];
            return token;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
