using System;
using System.Reflection;

namespace Landing.Model.Logging
{
    /// <summary>
    /// Обработка логирования 
    /// </summary>
    public class LogHandler : ILog
    {
        public delegate void LogEventHandler(LogModel model);
        private static event LogEventHandler _logEventChanged;
        /// <summary>
        /// Добавляем или удаляем подписку на событие лога
        /// </summary>
        public static event LogEventHandler LogEventChanged
        {
            add 
            { 
                if(_logEventChanged == null)
                {
                    _logEventChanged += value;
                }
                else
                {
                    foreach(var item in _logEventChanged.GetInvocationList())
                    {
                        if (!item.GetMethodInfo().Name.Contains(value.Method.Name))
                        {
                            _logEventChanged += value;
                        }

                    }

                }
            }
            remove { _logEventChanged -= value; }
        }
        /// <summary>
        /// Вывод
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="appname">Приложение/метод</param>
        public void Debug(string message, string appname)
        {
            LogModel model = new LogModel
            {
                Message = message,
                Method = appname,
                Type = "Debug"
            };
            _logEventChanged?.Invoke(model);
        }
        /// <summary>
        /// Вывод ошибка
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="appname">Приложение/метод</param>
        /// <param name="ex">Исключение</param>
        public void Error(string message, string appname, Exception ex)
        {
            LogModel model = new LogModel
            {
                Message = message,
                Method = appname,
                Type = "Error",
                Exception = ex
            };
            _logEventChanged?.Invoke(model);
        }
        /// <summary>
        /// Вывод фатальная ошибка
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="appname">приложение/метод</param>
        /// <param name="ex">Исключение</param>
        public void Fatal(string message, string appname, Exception ex)
        {
            LogModel model = new LogModel
            {
                Message = message,
                Method = appname,
                Type = "Fatal",
                Exception = ex
            };
            _logEventChanged?.Invoke(model);
        }
        /// <summary>
        /// Вывод информации
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="appname">Приложение/метод</param>
        public void Info(string message, string appname)
        {
            LogModel model = new LogModel
            {
                Message = message,
                Method = appname,
                Type = "Info"
            };
            _logEventChanged?.Invoke(model);
        }
        /// <summary>
        /// Вывод опасность
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="appname">Приложение/метод</param>
        /// <param name="ex">Исключение</param>
        public void Warning(string message, string appname, Exception ex)
        {
            LogModel model = new LogModel
            {
                Message = message,
                Method = appname,
                Type = "Warning",
                Exception = ex
            };
            _logEventChanged?.Invoke(model);
        }
    }
}
