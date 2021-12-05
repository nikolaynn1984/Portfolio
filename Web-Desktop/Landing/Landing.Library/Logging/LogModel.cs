using System;

namespace Landing.Library.Logging
{
    /// <summary>
    /// Модель логирования
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Метод - от куда вызвано сообщения
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// Тип лога
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Исключение
        /// </summary>
        public Exception Exception { get; set; }
    }
}
