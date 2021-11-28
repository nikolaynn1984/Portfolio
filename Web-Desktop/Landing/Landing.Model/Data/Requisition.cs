using Landing.Model.Notifity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Landing.Model.Data
{
    /// <summary>
    /// Заявки
    /// <para>Поля</para>
    /// Id, StatusId, Name, Email, Date, Agree
    /// </summary>
    public class Requisition : BaseNotifiry
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
        /// Идентификатор Статуса
        /// </summary>
        public  int StatusId
        {
            get
            {
                return this.statusid;
            }
            set
            {
                this.statusid = value;
                OnPropertyChanged(nameof(this.StatusId));
            }
        }
        /// <summary>
        /// Имя клиента
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Поле не может содержать меньше 3х и больше 50 символов")]
        public  string Name
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
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [EmailAddress(ErrorMessage = "Не корректный Емаил")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Поле не может содержать меньше 5х и больше 50 символов")]
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
                OnPropertyChanged(nameof(this.Email));
            }
        }
        /// <summary>
        /// Дата заявки
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
                OnPropertyChanged(nameof(this.Date));
            }
        }
        /// <summary>
        /// Номер заявки
        /// </summary>
        public int OrderNumber
        {
            get
            {
                return this.ordernumber;
            }
            set
            {
                this.ordernumber = value;
                OnPropertyChanged(nameof(this.OrderNumber));
            }
        }

        /// <summary>
        /// Согласен на обработку персональных данных
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public bool Agree
        {
            get
            {
                return this.agree;
            }
            set
            {
                this.agree = value;
                OnPropertyChanged(nameof(this.Agree));
            }
        }
        /// <summary>
        /// Описание заявки
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(500, ErrorMessage ="Поле не должно первышать 500 символов")]
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
        /// Статус
        /// </summary>
        [ForeignKey("StatusId")]
        public  RequisitionStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
                OnPropertyChanged(nameof(this.Status));
            }
        }

        /// <summary>
        /// Поле - идентификатор
        /// </summary>
        private int id;
        /// <summary>
        /// Поле - идентификатор статуса
        /// </summary>
        private int statusid;
        /// <summary>
        /// Поле - имя клиента
        /// </summary>
        private string name;
        /// <summary>
        /// Поле - email
        /// </summary>
        private string email;
        private DateTime date;
        /// <summary>
        /// Поле - номер заявки
        /// </summary>
        private int ordernumber;
        /// <summary>
        /// Поле - согласен на обработку персоныльных данных
        /// </summary>
        private bool agree;
        /// <summary>
        /// Поле - описание заявки/сообщение
        /// </summary>
        private string description;
        /// <summary>
        /// Поле - статус
        /// </summary>
        private RequisitionStatus status;
        private Requisition model;

        public Requisition(Requisition model)
        {
            this.model = model;
        }

        public Requisition()
        {
        }
    }
}
