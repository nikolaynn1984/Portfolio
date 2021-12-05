using Landing.Library.Notifity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Landing.Library.Model
{
    /// <summary>
    /// Блог
    /// </summary>
    public class Blog : BaseNotifiry
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
        /// Название/Заголовок
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Поле не может содержать меньше 3х и больше 50 символов")]
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
        /// Описание блога
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
        /// Дата добавления блога
        /// </summary>
        public DateTime CreadDate
        {
            get
            {
                return this.creaddate;
            }
            set
            {
                this.creaddate = value;
                OnPropertyChanged(nameof(this.CreadDate));
            }
        }

        /// <summary>
        /// Идентификатор картинки
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
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
        /// Модель изображения
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


        private int id;
        private string name;
        private string description;
        private DateTime creaddate;
        private int imageid;
        private Images getimage;
    }
}
