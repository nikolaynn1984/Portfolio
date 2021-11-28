using Landing.Model.Notifity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Landing.Model.Data
{
    [Table("MessageUser")]
    public class MessageUser : BaseNotifiry
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id
        {
            get { return this.id; }
            set {  
                this.id = value; 
                OnPropertyChanged(nameof(this.Id));
            }
        }
        /// <summary>
        /// Ник пользователя
        /// </summary>
        public string UserName
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
                OnPropertyChanged(nameof(this.UserName));
            }
        }
        /// <summary>
        /// Расположение папки
        /// </summary>
        public long UserId
        {
            get
            {
                return this.userid;
            }
            set
            {
                this.userid = value;
                OnPropertyChanged(nameof(this.UserId));
            }
        }
        /// <summary>
        /// Дата добавления пользователя
        /// </summary>
        public DateTime AddDate
        {
            get
            {
                return this.adddate;
            }
            set
            {
                this.adddate = value;
                OnPropertyChanged(nameof(this.AddDate));
            }
        }


        /// <summary>
        /// Поле - идентификатор
        /// </summary>
        private int id;
        /// <summary>
        /// Ник пользователя
        /// </summary>
        private string username;
        /// <summary>
        /// Расположение папки
        /// </summary>
        private long userid;
        /// <summary>
        /// Поле - Дата добавления пользователя
        /// </summary>
        private DateTime adddate;
    }
}
