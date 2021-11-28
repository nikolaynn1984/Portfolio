using System;

namespace Landing.Model.Logging
{
    /// <summary>
    /// Переопределение лога в обработчик
    /// </summary>
    public static class Log
    {
        private static ILog logHandler;
        static Log()
        {
            logHandler = new LogHandler();
        }

        /// <summary>
        /// Вывод информация
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="method">Приложение/метод</param>
        public static void Info(string message, string method)
        {
            logHandler.Info(message, method);
        }
        /// <summary>
        /// Простой вывод
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="method">Приложение/метод</param>
        public static void Debug(string message, string method)
        {
            logHandler.Debug(message, method);
        }
        /// <summary>
        /// Вывод опасность
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="method">Приложение/метод</param>
        /// <param name="ex">Исключение</param>
        public static void Warn(string message, string method, Exception ex)
        {
            logHandler.Warning(message, method, ex);
        }
        /// <summary>
        /// Вывод ошибка
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="method">Приложение</param>
        /// <param name="ex">Исключение</param>
        public static void Error(string message, string method, Exception ex)
        {
            logHandler.Error(message, method, ex);
        }
        /// <summary>
        /// Вывод, фатальная ошибка
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="method">Приложение/метод</param>
        /// <param name="ex">Исключение</param>
        public static void Fatal(string message, string method, Exception ex)
        {
            logHandler.Fatal(message, method, ex);
        }
    }
}
