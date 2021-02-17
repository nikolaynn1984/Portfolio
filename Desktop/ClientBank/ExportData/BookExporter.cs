using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportData
{
    public class BookExporter
    {
        /// <summary>
        /// Тип файла
        /// </summary>
        public IBookSave Mode { get; set; }
        /// <summary>
        /// Конструктов
        /// </summary>
        /// <param name="book">Модель типа файла</param>
        public BookExporter(IBookSave book)
        {
            this.Mode = book;
        }
        /// <summary>
        /// Сохранение данных в файл
        /// </summary>
        public void Save()
        {
            Mode.SaveBookPages();
        }
    }
}
