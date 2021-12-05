using System;
using System.Threading.Tasks;
using Landing.Library.Model;
using Microsoft.AspNetCore.SignalR.Client;

namespace Landing.Repository.Hub
{
    /// <summary>
    /// Подписка на события
    /// </summary>
    public class Message
    {
        private readonly HubConnection connection;

        public Message(string url, string token)
        {
            this.connection = new HubConnectionBuilder().WithUrl(url + "landhub", option => 
            {
                option.AccessTokenProvider = () => Task.FromResult( token);
            }).Build();
            IsConnected = false;
            connection.On<int>("EditRequest", id => EditRequest(id));
            connection.On<MessagesBot>("BotMessage", mess => AddMessages(mess));
            connection.On<bool>("ConnectionBot", result => BotConnection(result));
        }
        /// <summary>
        /// Получаем информацию если соединение с ботом или нет
        /// </summary>
        /// <param name="result">Результат</param>
        private void BotConnection(bool result)
        {
            RMessageBot.SetBotConnection(result);
        }
        /// <summary>
        /// Получаем сообщение в боте
        /// </summary>
        /// <param name="mess">Сообщение</param>
        private void AddMessages(MessagesBot mess)
        {
            RMessageBot.Create(mess);
        }
        /// <summary>
        /// Завяка пользователя
        /// </summary>
        /// <param name="id">Идентификатор</param>
        private void EditRequest(int id)
        {
            RRequisition.Create(id);
        }


        /// <summary>
        /// Соединение с Хабом
        /// </summary>
        /// <returns></returns>
        public async Task Connect()
        {
            if (IsConnected) return;
            try
            {
                await connection.StartAsync();
                IsConnected = true;
            }
            catch
            {
                Failt.ErrorsHandler.AddMessage("Ошибка подключения");
                IsConnected = false;
            }
        } 
        /// <summary>
        /// Отключение соединения с Хабом
        /// </summary>
        /// <returns></returns>
        public async Task Disconnect()
        {
            if (!isConnected) return;
            await connection.StopAsync();
            IsConnected = false;
        }


        /// <summary>
        /// Соединение, да или нет
        /// </summary>
        private static bool isConnected;
        public static bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; }
        }
    }
}
