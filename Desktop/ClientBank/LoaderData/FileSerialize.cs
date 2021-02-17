using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoaderData
{
    /// <summary>
    /// Сериализации и десериализации данных
    /// </summary>
    internal class FileSerialize
    {
        /// <summary>
        /// Метод сериализации данных
        /// </summary>
        /// <param name="template">Модель данных</param>
        /// <param name="name">Название файла</param>
        internal static void Serialize<T>(ObservableCollection<T> template, string name)
        {
            string path = "Files/" + name + ".json";
            string json = JsonConvert.SerializeObject(template);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Метод десериализации данных
        /// </summary>
        /// <param name="template">Модель данных</param>
        /// <param name="name">Название файла</param>
        /// <returns>Модель данных</returns>
        internal static ObservableCollection<T> Deserialize<T>(ObservableCollection<T> template, string name)
        {
            string path = "Files/" + name + ".json";
            bool result = File.Exists(path);

            if (result)
            {
                string json = File.ReadAllText(path);
                template = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            }

            return template;
        }

    }
}
