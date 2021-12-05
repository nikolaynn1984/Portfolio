using Landing.Library.Model;
using Landing.Library.Interfaces;
using Landing.Library.Logging;
using Landing.Server.Database.Query;
using Landing.Server.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Landing.Server.Data
{
    /// <summary>
    /// Обработка данных телеграмм бота
    /// </summary>
    public class MessagesBotData : IMessagesBot
    {
        private readonly IHubContext<LandingHub> hubContext;
        private readonly BaseMessagesBot baseMessages;
        private readonly BaseImages baseImages;
        public  string connection;
        public static bool ConnectionBot;
        public MessagesBotData(IHubContext<LandingHub> hubContext)
        {
            this.connection = Connection.GetConnectionString();
            this.baseImages = new BaseImages(connection);
            this.baseMessages = new BaseMessagesBot(connection);
            this.hubContext = hubContext;
        }




        /// <summary>
        /// Добавляем пользователя если нет в базе
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        /// <returns>Пользователь</returns>
        public async Task<MessageUser> AddUser(MessageUser user)
        {
            try
            {
                MessageUser userHas = await baseMessages.GetUsersByUserId(user.UserId);
                if (userHas != null)
                {
                    if(userHas.UserId == 0)
                    {
                        userHas = await baseMessages.AddUser(user);
                    }

                    return userHas;
                }
                else
                {
                    return userHas;
                }

            }
            catch(Exception e)
            {
                Log.Error("Ошибка добалвния пользователя TelegrammBot", "MessagesBotData/AddUser", e);
                return null;
            }
        }
        /// <summary>
        /// Получаем список сообщений
        /// </summary>
        /// <returns>Список сообщений</returns>
        public async Task<IEnumerable<MessagesBot>> GetMessages()
        {
            List<MessagesBot> list = await baseMessages.GetMessages();
            List<Images> images = await baseImages.GetItems();
            foreach (MessagesBot item in list.Where(s => s.MessgeType == "Photo"))
            {
                item.GetImages = images.FirstOrDefault(s => s.Id == item.ImageId);
            }
            return list;
        }
        /// <summary>
        /// Получаем сообщение по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сообщение</returns>
        public async Task<MessagesBot> GetMessagesById(int id)
        {
            var images = await baseImages.GetItemByLocation("Messages");
            MessagesBot messages = await baseMessages.GetMessagesById(id);
            if (messages.MessgeType == "Photo")
            {
                messages.GetImages = images.SingleOrDefault(s => s.Id == messages.ImageId);
            }
            return messages;
        }
        /// <summary>
        /// Получаем список пользователей
        /// </summary>
        /// <returns>Список пользователей бота</returns>
        public async Task<IEnumerable<MessageUser>> GetUsers()
        {
            return await baseMessages.GetUsers();    
        }
        /// <summary>
        /// Получаем информацию пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Объект MessageUser </returns>
        public async Task<MessageUser> GetUsersById(int id)
        {
            return await baseMessages.GetUsersById(id);
        }
        /// <summary>
        /// Добавляем сообщение и отправляем информацию по подписке
        /// </summary>
        /// <param name="messages">Сообщение</param>
        /// <returns>MessagesBot с присвоеным Id если добалвение прошло успешно, в противном случае false</returns>
        public async Task<MessagesBot> AddMessage(MessagesBot messages)
        {
            try
            {
                messages = await baseMessages.AddMessage(messages);
                if(messages != null)
                {
                    
                    if (messages.MessgeType == "Photo")
                    {
                        messages.GetImages = await baseImages.GetItemById(messages.ImageId);
                    }
                    await hubContext.Clients.All.SendAsync("BotMessage", messages);
                }
                return messages;
            }
            catch(Exception ex)
            {
                Log.Error("Ошибка добавления сообщения", "AddMessage", ex);
                return null;
            }
        }
        /// <summary>
        /// Загрузка списка сообщений по идентификатору пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Список сообщений</returns>
        public async Task<IEnumerable<MessagesBot>> GetMessagesByUserId(int id)
        {
            List<MessagesBot> list = await baseMessages.GetMessagesByUserId(id);
            List<Images> images = await baseImages.GetItems();
            foreach (MessagesBot item in list.Where(s => s.MessgeType == "Photo"))
            {
                item.GetImages = images.FirstOrDefault(s => s.Id == item.ImageId);
            }
            return list;
        }
        /// <summary>
        /// Отправляем информацию о соединении 
        /// </summary>
        /// <param name="result">Рузультат</param>
        public async void BotConnection(bool result, string name)
        {
            ConnectionBot = result;
            await hubContext.Clients.All.SendAsync("ConnectionBot", result);
            if(result) Log.Info($"Соединение с телеграмм ботом установлено - {name}", "BotConnection");
            else Log.Info($"Соединение с телеграмм ботом разорвано", "BotConnection");
        }
        /// <summary>
        /// Проверка соединения
        /// </summary>
        /// <returns></returns>
        public  bool CheckConnection()
        {
            return ConnectionBot;
        }
        /// <summary>
        /// Получаем информацию пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Объект MessageUser </returns>
        public async Task<MessageUser> GetUsersByUserId(long id)
        {
            return await baseMessages.GetUsersByUserId(id);
        }
    }
}
