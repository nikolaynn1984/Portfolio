using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Landing.Server.Data
{
    /// <summary>
    /// Соединение с базой данных
    /// </summary>
    public class Connection : IHostedService
    {
        private static IConfiguration Configuration;

        public Connection(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Получаем строку подключения
        /// </summary>
        /// <returns>Подключение</returns>
        public static string GetConnectionString()
        {
            return Configuration.GetConnectionString("DefaultConnection");
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
