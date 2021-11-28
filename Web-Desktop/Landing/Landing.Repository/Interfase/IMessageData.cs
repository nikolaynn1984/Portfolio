using Landing.Model.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Repository.Interfase
{
    interface IMessageData
    {
        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        Task<IEnumerable<MessageUser>> GetUsers();
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
        /// Получаем информацию о пользователе
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Объект MessageUser с информацией о пользователе</returns>
        Task<MessageUser> GetUserById(int id);
        /// <summary>
        /// Проверяем тукущее состояние подключение к телеграмм боку
        /// </summary>
        /// <returns>true - если соединение установлено, в противном случае false</returns>
        Task<bool> CheckConnection();

    }


}
