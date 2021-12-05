using Landing.Library.Model;
using System.Threading.Tasks;

namespace Landing.Library.Interfaces
{
    public interface IPageView
    {
        /// <summary>
        /// Данные гравного онкна
        /// </summary>
        /// <returns>параметры/returns>
        Task<PageView> Get();
        /// <summary>
        /// Редактировать параметры гравного окна
        /// </summary>
        /// <param name="model">Модель окна</param>
        /// <returns>true - если прошло успешно, в противном случа false</returns>
        Task<bool> Edit(PageView model);
    }
}
