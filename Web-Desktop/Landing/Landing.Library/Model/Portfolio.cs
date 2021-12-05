using Landing.Library.Notifity;
using System.ComponentModel.DataAnnotations;

namespace Landing.Library.Model
{
    /// <summary>
    /// Портфолио
    /// </summary>
    public class Portfolio : BaseNotifiry
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
        /// Заголовок
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(150, ErrorMessage = "Поле не должно превышать 150 символов")]
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
        /// Описание
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
                OnPropertyChanged(nameof(this.Description));
            }
        }
        /// <summary>
        /// Идентификатор картинки
        /// </summary>
        [Required(ErrorMessage = "Требуется изображение")]
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
        /// Поле идентификатор
        /// </summary>
        private int id;
        /// <summary>
        /// Поле - Название/Заголовок
        /// </summary>
        private string name;
        /// <summary>
        /// Поле - описание
        /// </summary>
        private string description;
        /// <summary>
        /// Поле - идентификатор изображения
        /// </summary>
        private int imageid;
        /// <summary>
        /// Изображения
        /// </summary>
        private Images getimage;
    }
}
