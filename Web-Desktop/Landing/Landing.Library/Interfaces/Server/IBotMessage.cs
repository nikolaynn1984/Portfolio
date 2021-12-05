using Landing.Library.Model;
using System.Threading.Tasks;

namespace Landing.Library.Interfaces.Server
{
    public interface IBotMessage
    {
        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="messages">Объект MessagesBot с данными</param>
        Task<MessagesBot> Send(MessagesBot messages);
    }
}
