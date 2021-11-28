using Landing.Model.Data;
using Landing.Repository.Interfase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Landing.Repository.Loader
{
    /// <summary>
    /// Клиент, Телеграмм бот
    /// Все данные влючаея пользователй относятся только к телеграмм боту
    /// </summary>
    class BotMessage : IMessageData
    {
        private HttpClient client;
        public BotMessage(string server, string token)
        {
            this.client = new HttpClient();
            client.BaseAddress = new Uri(server + "api/MessageBot/"); //api/MessageBot/GetUsers
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
        /// <summary>
        /// Отправка сообщения 
        /// </summary>
        /// <param name="messages">Сообщение</param>
        /// <returns>Модель сообщения с идентификатором</returns>
        public async Task<MessagesBot> AddMessage(MessagesBot messages)
        {
            try
            {
                var responce = await client.PostAsJsonAsync("Send", messages);
                if (responce.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<MessagesBot>(await responce.Content.ReadAsStringAsync());
                    return result;
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return null;
                }
            }
            catch (Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Проверяем соодение с ботом
        /// </summary>
        /// <returns>true - если содение, в противном случа false</returns>
        public async Task<bool> CheckConnection()
        {
            try
            {
                var responce = await client.GetAsync("Connection");
                if (responce.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<bool>(responce.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return false;
                }
            }
            catch (Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return false;
            }
        }
        /// <summary>
        /// Получаем список сообщений по идентификатору пользователя
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Список сообщений</returns>
        public async Task<IEnumerable<MessagesBot>> GetMessagesByUserId(int id)
        {
            try
            {
                var responce = await client.GetAsync($"GetByUserId/{id}");
                if (responce.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<MessagesBot>>(responce.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return null;
                }
            }
            catch (Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Получаем информацию о пользователе по идентификатору (Только для телеграмм бота)
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель пользователя</returns>
        public async Task<MessageUser> GetUserById(int id)
        {
            try
            {
                var responce = await client.GetAsync($"GetUserById/{id}");
                if (responce.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<MessageUser>(responce.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return null;
                }
            }
            catch (Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Получаем список пользователй чата
        /// </summary>
        /// <returns>Список</returns>
        public async Task<IEnumerable<MessageUser>> GetUsers()
        {
            try
            {
                var responce = await client.GetAsync("GetUsers");
                if (responce.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<MessageUser>>(responce.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return null;
                }
            }
            catch (Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return null;
            }
        }
    }
}
