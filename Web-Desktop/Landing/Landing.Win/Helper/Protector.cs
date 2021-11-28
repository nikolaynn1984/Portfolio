using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Landing.Win.Helper
{
    /// <summary>
    /// Шифрование паролья пользовател
    /// </summary>
    class Protector
    {
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("LANDING20");
        private static readonly string Secret = "Landing2021!";


        private static readonly int iteration = 2000;
        /// <summary>
        /// Шифрование
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Токен</returns>
        public static string Encrypt(string password)
        {
            try
            {
                byte[] plainByte = Encoding.Unicode.GetBytes(password);

                var aes = Aes.Create();
                var pbkdf2 = new Rfc2898DeriveBytes(Secret, salt, iteration);
                aes.Key = pbkdf2.GetBytes(32);
                aes.IV = pbkdf2.GetBytes(16);

                var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plainByte, 0, plainByte.Length);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// Расшифровываем пароль пользователя
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Пароль</returns>
        public static string Decrypt(string password)
        {
            try
            {
                byte[] cryptoBytes = Convert.FromBase64String(password);
                var aes = Aes.Create();
                var pbkdf2 = new Rfc2898DeriveBytes(Secret, salt, iteration);
                aes.Key = pbkdf2.GetBytes(32);
                aes.IV = pbkdf2.GetBytes(16);
                var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                }
                return Encoding.Unicode.GetString(ms.ToArray());
            }
            catch
            {
                return null;
            }
           
        }
    }
}
