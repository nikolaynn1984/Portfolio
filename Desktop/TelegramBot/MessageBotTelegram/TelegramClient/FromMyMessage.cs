using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClient
{
    public static class FromMyMessage
    {
        private static Client client;
        static FromMyMessage()
        {
        }
        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="token">Токен бота</param>
        /// <param name="Text">Текст сообщения</param>
        /// <param name="UserId">ИД Пользователя</param>
        /// <param name="nick">Ник пользователя</param>
        public static void NewMessage(string token,string Text, long UserId, string nick)
        {
            client = new Client(token);
            client.SendMessage(Text, UserId, nick);
        }

    }
}
