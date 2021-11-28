using Landing.Interface;
using Landing.Model.Data;
using Landing.Server.Auth;
using Landing.Server.Intarfase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Landing.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class MessageBotController : ControllerBase
    {
        private readonly IMessagesBot data;
        private readonly IBotMessage send;
        public MessageBotController(IMessagesBot data, IBotMessage send)
        {
            this.data = data;
            this.send = send;
        }
        /// <summary>
        /// Получаем список пользователе телеграмм бота
        /// </summary>
        /// <returns>Список MessageUser</returns>
        [HttpGet]
        [Route("api/MessageBot/GetUsers")]
        public async Task<ActionResult<IEnumerable<MessageUser>>> GetUsers()
        {
            bool result = User.IsInRole(LRols.Admin.ToString());
            if (!result)
            {
                return Unauthorized();
            }
            IEnumerable<MessageUser> list = await data.GetUsers();
            if (list == null)
            {
                ModelState.AddModelError("GetUsers", "Не удалось получить список пользователей");
                return BadRequest(ModelState);
            }
            return list.ToList();
        }
        /// <summary>
        /// Проверяем соодинение с телеграмм ботом
        /// </summary>
        /// <returns>true - если соединеие успешно, в противном случае false</returns>
        [HttpGet]
        [Route("api/MessageBot/Connection")]
        public ActionResult<bool> Connection()
        {
            bool result = User.IsInRole(LRols.Admin.ToString());
            if (!result)
            {
                return Unauthorized("Отказано в доступе");
            }
            return data.CheckConnection();
        }
        /// <summary>
        /// Получаем сообщени по идентификатору пользователя
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Список MessagesBot</returns>
        [HttpGet]
        [Route("api/MessageBot/GetByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<MessagesBot>>> GetByUserId(int id)
        {
            bool result = User.IsInRole(LRols.Admin.ToString());
            if (!result)
            {
                return Unauthorized();
            }
            IEnumerable<MessagesBot> list = await data.GetMessagesByUserId(id);
            if (list == null)
            {
                ModelState.AddModelError("GetByUserId", "Не удалось получить список сообщений");
                return BadRequest(ModelState);
            }
            return list.ToList();
        }
        /// <summary>
        /// Получаем информацию о пользователе по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель MessageUser</returns>
        [HttpGet]
        [Route("api/MessageBot/GetUserById/{id}")]
        public async Task<ActionResult<MessageUser>> GetUserById(int id)
        {
            bool result = User.IsInRole(LRols.Admin.ToString());
            if (!result)
            {
                return Unauthorized();
            }
            MessageUser item = await data.GetUsersById(id);
            if (item == null)
            {
                ModelState.AddModelError("GetUsersById", "Не удалось получить информацию о пользователе");
                return BadRequest(ModelState);
            }
            return item;
        }
        /// <summary>
        /// Отправляем сообщение и добавляем в базу 
        /// </summary>
        /// <param name="messages">Сообщение</param>
        /// <returns>Модель MessagesBot</returns>
        [HttpPost]
        [Route("api/MessageBot/Send")]
        public async Task<ActionResult<MessagesBot>> Send([FromBody] MessagesBot messages)
        {
            bool result = User.IsInRole(LRols.Admin.ToString());
            if (!result)
            {
                return Unauthorized();
            }
            messages.SendType = "To";
            MessagesBot mes = await send.Send(messages);
            
            if (mes == null)
            {
                ModelState.AddModelError("Send", "Не удачная отправка сообщения");
                return BadRequest(ModelState);
            }
            return mes;
        }
    }
}
