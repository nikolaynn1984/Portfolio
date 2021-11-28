using System;
using System.IO;
using System.Security.Cryptography;

namespace Landing.Model.Hash
{
    public class HaxCodeFile
    {
        /// <summary>
        /// Получение Хэш кода
        /// </summary>
        /// <param name = "stream" > Filestrem с данными</param>
        /// <returns>Хэш код</returns>
        public static string GetSHA1(FileStream stream)
        {
            string sha = String.Empty;
            using (SHA1 SHA = new SHA1Managed())
            {
                sha = BitConverter.ToString(SHA.ComputeHash(stream));
            };
            stream.Close();
            return sha;
        }
        /// <summary>
        /// Получение Хэш кода
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Хэш код</returns>
        public static string GetSHA1(string path)
        {


            string sha = String.Empty;
            using(var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using(SHA1 SHA = new SHA1Managed())
                {
                    sha = BitConverter.ToString(SHA.ComputeHash(stream));
                }
            }
            return sha;
        }
    }
}
