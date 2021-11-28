using Landing.Model.Notifity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Landing.Model.Data
{
    /// <summary>
    /// Соцсети
    /// </summary>
    public class Social : BaseNotifiry
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged(nameof(this.Id));
            }
        }
        /// <summary>
        /// Название/описание ссылки
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                OnPropertyChanged(nameof(this.Name));
            }
        }
        /// <summary>
        /// Ссылка
        /// </summary>
        public string Link
        {
            get
            {
                return this.link;
            }
            set
            {
                this.link = value;
                OnPropertyChanged(nameof(this.Link));
            }
        }
        /// <summary>
        /// Идентификатор картинки
        /// </summary>
        public int ImageId
        {
            get
            {
                return this.imageid;
            }
            set
            {
                this.imageid = value;
                OnPropertyChanged(nameof(this.ImageId));
            }
        }
        /// <summary>
        /// Изображение
        /// </summary>
        public Images GetImages
        {
            get
            {
                return this.getimage;
            }
            set
            {
                this.getimage = value;
                OnPropertyChanged(nameof(this.GetImages));
            }
        }

        /// <summary>
        /// Поле - идентификатор
        /// </summary>
        private int id;
        /// <summary>
        /// Поле - название/описание ссылки
        /// </summary>
        private string name;
        /// <summary>
        /// Поле - ссылка
        /// </summary>
        private string link;
        /// <summary>
        /// Поле - идентификатор изображения
        /// </summary>
        private int imageid;
        /// <summary>
        /// Поле изображение
        /// </summary>
        private Images getimage;
    }
}
