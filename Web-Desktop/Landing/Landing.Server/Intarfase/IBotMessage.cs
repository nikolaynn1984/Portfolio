using Landing.Model.Data;
using System.Threading.Tasks;

namespace Landing.Server.Intarfase
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
