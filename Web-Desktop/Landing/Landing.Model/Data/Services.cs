using Landing.Model.Notifity;
using System.ComponentModel.DataAnnotations;

namespace Landing.Model.Data
{
    /// <summary>
    /// Список услуг
    /// </summary>
    public class Services : BaseNotifiry
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
        /// Услуга
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Поле не должно превышать 50 символов")]
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
        /// Поле - идентификатор
        /// </summary>
        private int id;
        /// <summary>
        /// Поле - название услуги
        /// </summary>
        private string name;
        /// <summary>
        /// Поле - описание
        /// </summary>
        private string description;
    }
}
