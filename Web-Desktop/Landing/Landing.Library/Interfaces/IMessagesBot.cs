using Landing.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Library.Interfaces
{
    /// <summary>
    /// Сообщения бота
    /// </summary>
    public interface IMessagesBot
    {
        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        Task<IEnumerable<MessageUser>> GetUsers();
        /// <summary>
        /// Получаем информаци о пользователе по идентификатору
        /// </summary>
        /// <returns>Объек MessageUser с данными</returns>
        Task<MessageUser> GetUsersById(int id);
        /// <summary>
        /// Получить список сообщений
        /// </summary>
        /// <returns>Список сообщений</returns>
        Task<IEnumerable<MessagesBot>> GetMessages();
        /// <summary>
        /// Получаем список по идентификатору пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Список сообщений</returns>
        Task<IEnumerable<MessagesBot>> GetMessagesByUserId(int id);
        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="messages">Модель сообщения</param>
        Task<MessagesBot> AddMessage(MessagesBot messages);
        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        Task<MessageUser> AddUser(MessageUser user);
        /// <summary>
        /// Сообщение по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сообщения</param>
        /// <returns>Сообщение</returns>
        Task<MessagesBot> GetMessagesById(int id);
        Task<MessageUser> GetUsersByUserId(long id);
        /// <summary>
        /// Информируем о текущем состоянии 
        /// </summary>
        /// <param name="result">Результа</param>
        void BotConnection(bool result, string name);
        /// <summary>
        /// Проверяем подключение к боту
        /// </summary>
        /// <returns>true - если соединение установлено, в противном случа false</returns>
        bool CheckConnection();

    }
}
