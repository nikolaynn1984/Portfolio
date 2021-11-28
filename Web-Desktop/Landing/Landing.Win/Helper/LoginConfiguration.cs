using Newtonsoft.Json;
using System.IO;

namespace Landing.Win.Helper
{
    /// <summary>
    /// Хранение логина и паролья пользователя
    /// </summary>
    public class LoginConfiguration
    {
        private static readonly string path = "configurelogin.json";
        /// <summary>
        /// Сохраняем данные
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="passord">пароль</param>
        public static void Save(string login, string passord)
        {
            var encrypt = Protector.Encrypt(passord);

            var item = new SaveLogin()
            {
                Email = login,
                Token = encrypt
            };

            string json = JsonConvert.SerializeObject(item);
            File.WriteAllText(path, json);
        }
        /// <summary>
        /// Читаем данные о пользователя если сохранены
        /// </summary>
        /// <returns>Модель SaveLogin</returns>
        public static SaveLogin Get()
        {
            if (!File.Exists(path)) return null;
            SaveLogin info = new SaveLogin();
            string json = File.ReadAllText(path);

            info = JsonConvert.DeserializeObject<SaveLogin>(json);
            var password = Protector.Decrypt(info.Token);
            info.Token = password;
            return info;
        }
    }


    public class SaveLogin
    {
        /// <summary>
        /// Емаил
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Токен, зашифрованный пароль
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Домен
        /// </summary>
        public string Domen { get; set; }
    }
}
