using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagesData.LoaderData
{
    internal class SerializeData
    {
        /// <summary>
        /// Метод сериализации данных
        /// </summary>
        /// <param name="template">Модель данных</param>
        /// <param name="name">Название файла</param>
        internal static void SaveItems<T>(ObservableCollection<T> template, Path pathselected)
        {

            string path = GetPath(pathselected);
            bool resul = File.Exists(path);
            if (!resul) Directory.CreateDirectory("Files");

            string json = JsonConvert.SerializeObject(template);
            File.WriteAllText(path, json);
 
            
        }

        /// <summary>
        /// Метод десериализации данных
        /// </summary>
        /// <param name="template">Модель данных</param>
        /// <param name="name">Название файла</param>
        /// <returns>Модель данных</returns>
        internal static ObservableCollection<T> GetItems<T>(ObservableCollection<T> template, Path pathselected)
        {
            string path = GetPath(pathselected);
            bool result = File.Exists(path);

            if (result)
            {
                string json = File.ReadAllText(path);
                template = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            }

            return template;
        }
        private static string GetPath(Path path)
        {
            string result = string.Empty;

            switch (path)
            {
                case (Path)1: result = "Users"; break; // Пользователи
                case (Path)2: result = "MessagesLog"; break; // Сообщения
            }

            return "Files/" + result + ".json";
        }


    }
}
