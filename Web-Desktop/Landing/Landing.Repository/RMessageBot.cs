using Landing.Model.Data;
using Landing.Repository.Interfase;
using Landing.Repository.Loader;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Repository
{
    public class RMessageBot 
    {

        private readonly IMessageData data;
        private static RImages images;
        public static event Action<MessagesBot> MessageBotEvent;
        public static event Action<bool> MessageConnectionEvent;
        public RMessageBot(string url, string token)
        {
            images = new RImages(url, token);
            this.data = new BotMessage(url, token);
        }
        /// <summary>
        /// Получаем сообщение по подписке
        /// </summary>
        /// <param name="mes">Сообщение</param>
        public static async Task Create(MessagesBot mes)
        {
            if (mes.MessgeType == "Photo")
            {
                if (mes.ImageId != 0)
                {
                    var image = await images.Download(mes.GetImages);
                }
            }
            MessageBotEvent?.Invoke(mes);
        }
        /// <summary>
        /// Получаем состояние соодения сервиса телеграмм бота
        /// по подписке
        /// </summary>
        /// <param name="result">Результат - true если соединение установлено, в противном случае false</param>
        public static void SetBotConnection(bool result)
        {
            MessageConnectionEvent?.Invoke(result);
        }
        /// <summary>
        /// Добавляем сообщение 
        /// </summary>
        /// <param name="messages">Модель сообщения</param>
        /// <returns>Объект MessagesBot с даными сообщения</returns>
        public async Task<MessagesBot> AddMessage(MessagesBot messages)
        {
            return await data.AddMessage(messages);
        }
        /// <summary>
        /// Добавление сообщения с изображением
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="path"></param>
        /// <returns>Объект MessagesBot с данными</returns>
        public async Task<MessagesBot> AddMessage(MessagesBot messages, string path)
        {
            string description = !string.IsNullOrEmpty(messages.MessageCaption) ? messages.MessageCaption : "Изображение чат бота";
            Images img = await images.Upload(path, FileLoacations.Messages, description);
            if (img != null)
            {
                messages.ImageId = img.Id;
                MessagesBot item = await data.AddMessage(messages);

                return item;
            }
            return null;
        }
        /// <summary>
        /// Получаем список сообщений по идентификатору пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Список сообщений</returns>
        public async Task<IEnumerable<MessagesBot>> GetMessagesByUserId(int id)
        {
            var list = await data.GetMessagesByUserId(id);
            if (list != null)
            {
                foreach (MessagesBot item in list)
                {
                    if (item.MessgeType == "Photo")
                    {
                        if (item.ImageId != 0)
                            await images.Download(item.GetImages);
                    }

                }
            }
            else return new List<MessagesBot>();
            return list;
        }
        /// <summary>
        /// Получаем список пользователей чат бота
        /// </summary>
        /// <returns>Пользователи</returns>
        public async Task<IEnumerable<MessageUser>> GetUsers()
        {
            return await data.GetUsers();
        }
        /// <summary>
        /// Получаем информацию о пользователе
        /// </summary>
        /// <param name="id">Идентификатор пользоватлея</param>
        /// <returns>Объект MessageUser с информацией о пользователе</returns>
        public async Task<MessageUser> GetUserById(int id)
        {
            return await data.GetUserById(id);
        }
        /// <summary>
        /// Проверяем тукущее состояние подключение к телеграмм боку
        /// </summary>
        /// <returns>true - если соединение установлено, в противном случае false</returns>
        public async Task<bool> CheckConnection()
        {
            return await data.CheckConnection();
        }
    }
}
