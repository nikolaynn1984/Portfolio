using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services
{
    /// <summary>
    /// Интерфейс Папки
    /// </summary>
    public interface IFolders
    {
        /// <summary>
        /// Получить список папок
        /// </summary>
        /// <returns>Список папок</returns>
        List<Folder> Get();
        /// <summary>
        /// Добавление папки или переименовывание если id > 0
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Название</param>
        /// <param name="parentid">Идентификатор подпапки</param>
        /// <param name="file">Выходные данные папки</param>
        /// <returns>false - если вызвано исключение, в противном случае true</returns>
        bool Add(int id, string name, int parentid, out Folder file);
        /// <summary>
        /// Удалени папки и все его корни
        /// </summary>
        /// <param name="id">Идентификатор папки</param>
        /// <returns>false - если вызвано исключение, в противном случае true</returns>
        bool Remove(int id);
    }
}
