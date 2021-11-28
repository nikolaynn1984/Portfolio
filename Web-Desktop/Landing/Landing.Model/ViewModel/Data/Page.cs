using Landing.Model.Data;

namespace Landing.Model.ViewModel.Data
{
    /// <summary>
    /// Страница
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Описание на баннере
        /// </summary>
        public string BannerName { get; set; }
        /// <summary>
        /// Кнопка на баннере
        /// </summary>
        public string BannerBtnName { get; set; }
        /// <summary>
        /// Путь картинки баннера
        /// </summary>
        public int BannerImageId { get; set; }
        /// <summary>
        /// Заголовок блока заявки
        /// </summary>
        public string ApplicationHeader { get; set; }
        /// <summary>
        /// Описание блока заявки
        /// </summary>
        public string ApplicationDescription { get; set; }

        public virtual Images GetImages { get; set; }
    }
}
