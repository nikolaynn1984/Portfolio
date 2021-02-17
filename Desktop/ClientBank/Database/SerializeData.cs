using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Database
{
    public class SerializeData
    {
        /// <summary>
        /// Метод сериализации данных
        /// </summary>
        /// <param name="template">Модель данных</param>
        /// <param name="name">Название файла</param>
        public static void SaveItems<T>(List<T> template, Path pathselected)
        {

            string path = GetPath(pathselected);
            string json = JsonConvert.SerializeObject(template);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Метод десериализации данных
        /// </summary>
        /// <param name="template">Модель данных</param>
        /// <param name="pathselected">Имя файла</param>
        /// <returns></returns>
        public static List<T> GetItems<T>(List<T> template, Path pathselected)
        {
            string path = GetPath(pathselected);
            bool result = File.Exists(path);

            if (result)
            {
                string json = File.ReadAllText(path);
                template = JsonConvert.DeserializeObject<List<T>>(json);
            }
            

            return template;
        }

        /// <summary>
        /// Метод сериализации данных
        /// </summary>
        /// <param name="template">Модель данных</param>
        /// <param name="name">Название файла</param>
        public static void SaveItems<T>(List<T> template, string name)
        {

            string path =  name;
            string json = JsonConvert.SerializeObject(template);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Метод десериализации данных
        /// </summary>
        /// <param name="template">Модель данных</param>
        /// <param name="name">Название файла</param>
        /// <returns>Модель данных</returns>
        public static List<T> GetItems<T>(List<T> template, string name)
        {
            string path = name;
            bool result = File.Exists(path);

            if (result)
            {
                string json = File.ReadAllText(path);
                template = JsonConvert.DeserializeObject<List<T>>(json);
            }

            return template;
        }

        private static string GetPath(Path path)
        {
            string result = string.Empty;

            switch (path)
            {
                case (Path)1: result = "Personal"; break; // Обычные счета
                case (Path)2: result = "BankAccount"; break; // Все счета
                case (Path)3: result = "Cities"; break; // Города
                case (Path)4: result = "Customers"; break; //Клиенты
                case (Path)5: result = "Business"; break; // Юр.лица - клиенты
                case (Path)6: result = "PhysicalClient"; break; // Физ.лица - клиенты
                case (Path)7: result = "AccountType"; break; //Тип счета
                case (Path)8: result = "Operations"; break; // Виды операций
                case (Path)9: result = "Repayment"; break; // Погашения кредитов
                case (Path)10: result = "Deposit"; break; // Вклады
                case (Path)11: result = "Credit"; break; //Кредиты
                case (Path)12: result = "Statuses"; break; //Статусы клиентов
                case (Path)13: result = "OperationHIstory"; break; // История операций
            }

            return "Files/" + result + ".json";
        }

      
    }
}
