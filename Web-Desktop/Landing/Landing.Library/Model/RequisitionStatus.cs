using Landing.Library.Notifity;
using System.ComponentModel.DataAnnotations;

namespace Landing.Library.Model
{
    /// <summary>
    /// Статусы заявок
    /// </summary>
    public class RequisitionStatus : BaseNotifiry
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
        /// Статус
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
        /// Описание статуса
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Поле не может содержать меньше 3х и больше 255 символов")]
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
        /// Поле идентификатор
        /// </summary>
        private int id;
        /// <summary>
        /// Поле - статус
        /// </summary>
        private string name;
        /// <summary>
        /// Поле - описание старуса
        /// </summary>
        private string description;
    }
}
