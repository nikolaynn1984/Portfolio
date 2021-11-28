using System;

namespace Landing.Model.Logging
{
    /// <summary>
    /// Логирование действий
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Вывод
        /// </summary>
        /// <param name="message">Сообщения</param>
        /// <param name="appname">Приложение</param>
        void Debug(string message, string appname);
        /// <summary>
        /// Вывод типо информация
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="appname">Приложение</param>
        void Info(string message, string appname);
        /// <summary>
        /// Вывод типа ошибка
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="appname">Приложение</param>
        /// <param name="ex">Исключение</param>
        void Error(string message, string appname, Exception ex);
        /// <summary>
        /// Вывод типа опасно
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="appname">Приложение</param>
        /// <param name="ex">Исключение</param>
        void Warning(string message, string appname, Exception ex);
        /// <summary>
        /// Вывод типа фальная ошибка
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="appname">Приложение</param>
        /// <param name="ex">Исключение</param>
        void Fatal(string message, string appname, Exception ex);
    }
}
