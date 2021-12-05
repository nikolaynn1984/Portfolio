using Landing.Library.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Landing.Server.Options
{
    /// <summary>
    /// Добавление логов
    /// </summary>
    public class ServerLogers : IHostedService
    {
        
        private readonly ILogger log;
        public ServerLogers(ILoggerFactory Log)
        {
            this.log = Log.CreateLogger("Landing");
            LogHandler.LogEventChanged += LogHandler_LogEventChanged;
        }

        private void LogHandler_LogEventChanged(LogModel model)
        {
            switch (model.Type)
            {
                case "Debug": Debug(model); break;
                case "Error": Error(model); break;
                case "Fatal": Fatal(model); break;
                case "Info": Info(model); break;
                case "Warning": Warning(model); break;
                default: break;
            }
        }

        public void Debug(LogModel model)
        {
            log.LogDebug($"{model.Method} | {model.Message}");
        }

        public void Error(LogModel model)
        {
            log.LogError(model.Exception, $"{model.Method} | {model.Message}");
        }

        public void Fatal(LogModel model)
        {
            log.LogCritical(model.Exception, $"{model.Method} | {model.Message}");
        }

        public void Info(LogModel model)
        {
            log.LogInformation($"{model.Method} | {model.Message}");
        }

        public void Warning(LogModel model)
        {
            log.LogWarning(model.Exception, $"{model.Method} | {model.Message}");
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
