using Landing.Library.Notifity;
using System;

namespace Landing.Library.Model
{
    public class MessagesBot : BaseNotifiry
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
        /// Идентификатор пользователя
        /// </summary>
        public int UserMessageId
        {
            get
            {
                return this.usermessageid;
            }
            set
            {
                this.usermessageid = value;
                OnPropertyChanged(nameof(this.UserMessageId));
            }
        }
        /// <summary>
        /// Идентификатор отправителя
        /// </summary>
        public long FromIdUser
        {
            get
            {
                return this.fromIduser;
            }
            set
            {
                this.fromIduser = value;
                OnPropertyChanged(nameof(this.FromIdUser));
            }
        }
        /// <summary>
        /// Дата и время добавления
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return this.createdate;
            }
            set
            {
                this.createdate = value;
                OnPropertyChanged(nameof(this.CreateDate));
            }
        }
        /// <summary>
        /// Тип отправки сообщений, от пользователя или пользователю
        /// </summary>
        public string SendType
        {
            get
            {
                return this.sendtyme;
            }
            set
            {
                this.sendtyme = value;
                OnPropertyChanged(nameof(SendType));
            }
        }
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public string MessgeType
        {
            get
            {
                return this.messgetype;
            }
            set
            {
                this.messgetype = value;
                OnPropertyChanged(nameof(this.MessgeType));
            }
        }
        /// <summary>
        /// Сообщение
        /// </summary>
        public string MessageText
        {
            get
            {
                return this.messagetext;
            }
            set
            {
                this.messagetext = value;
                OnPropertyChanged(nameof(this.MessageText));
            }
        }
        /// <summary>
        /// Идентификатор файла
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
        /// Подпись к файлу
        /// </summary>
        public string MessageCaption
        {
            get
            {
                return this.messagecaption;
            }
            set
            {
                this.messagecaption = value;
                OnPropertyChanged(nameof(this.MessageCaption));
            }
        }


        public virtual Images GetImages { get; set; }

        /// <summary>
        /// Поле - Идентификатор
        /// </summary>
        private int id;
        /// <summary>
        /// Поле - Идентификатор пользователя
        /// </summary>
        private int usermessageid;
        /// <summary>
        /// Поле - Идентификатор отправителя
        /// </summary>
        private long fromIduser;
        /// <summary>
        /// Тип отправки сообщений, от пользователя или пользователю
        /// </summary>
        private string sendtyme;
        /// <summary>
        /// Поле - Дата и время добавления
        /// </summary>
        private DateTime createdate;
        /// <summary>
        /// Поле - Тип сообщения
        /// </summary>
        private string messgetype;
        /// <summary>
        /// Поле - Сообщение
        /// </summary>
        private string messagetext;
        /// <summary>
        /// Поле - Идентификатор файла
        /// </summary>
        private int imageid;
        /// <summary>
        /// Поле - Подпись к файлу
        /// </summary>
        private string messagecaption;
    }
}
