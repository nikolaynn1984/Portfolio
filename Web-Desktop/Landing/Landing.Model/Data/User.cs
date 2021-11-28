using Microsoft.AspNetCore.Identity;

namespace Landing.Model.Data
{
    public class User : IdentityUser
    {

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Ссылка в телеграм
        /// </summary>
        public string Telegram { get; set; }
        /// <summary>
        /// Ссылка в вконтакте
        /// </summary>
        public string VKontakt { get; set; }
        /// <summary>
        /// Идентификатор фотографии
        /// </summary>
        public int ImageId { get; set; }
    }

    public class LandingUser
    {
        public int Id { get; set; }
    }
}
