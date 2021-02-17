using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MessagesData.Model
{
    public class MessagesLog : INotifyPropertyChanged
    {
        public static event Action<MessagesLog> CreateItems;

        /// <summary>
        /// Конструктор модели сообщений
        /// </summary>
        /// <param name="Id">ИД</param>
        /// <param name="UserId">Идентификатор пользователя</param>
        /// <param name="Name">Имя</param>
        /// <param name="CreateDate">Дата добавления</param>
        /// <param name="CreateTime">Время добавления сообщения</param>
        /// <param name="MessageType">Тип сообщения</param>
        /// <param name="MessageText">Текст сообщения</param>
        /// <param name="MessageCaption">Подпись к файлу</param>
        /// <param name="FileName">Имя файла</param>
        /// <param name="FromId">ОТправлено от</param>
        public MessagesLog(int Id, long UserId, string Name, DateTime CreateDate, string CreateTime, string MessageType,
            string MessageText, string MessageCaption, string FileName, long FromId)
        {
            this.id = Id;
            this.userId = UserId;
            this.name = Name;
            this.createDate = CreateDate;
            this.createTime = CreateTime;
            this.messageType = MessageType;
            this.messageText = MessageText;
            this.messageCaption = MessageCaption;
            this.fileName = FileName;
            this.fromId = FromId;
        }


        public void Create(MessagesLog log)
        {
            CreateItems?.Invoke(log);
        }


        /// <summary>
        /// Конструктор молеи сообщений
        /// </summary>
        /// <param name="Msg">Модель данных</param>
        public MessagesLog(MessagesLog Msg)
        {
            this.Id = Msg.Id;
            this.UserId = Msg.UserId;
            this.Name = Msg.Name;
            this.CreateDate = DateTime.Now;
            this.CreateTime = DateTime.Now.ToShortTimeString();
            this.MessageType = Msg.MessageType;
            this.MessageText = Msg.MessageText;
            this.MessageCaption = Msg.MessageCaption;
            this.FileName = Msg.FileName;
            this.FromId = Msg.FromId;
        }

        public MessagesLog()
        {
        }

        /// <summary>
        /// Ид
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(nameof(this.Id)); }
        }
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public long UserId
        {
            get { return this.userId; }
            set { this.userId = value; OnPropertyChanged(nameof(this.UserId)); }
        }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value;OnPropertyChanged(nameof(this.Name)); }
        }
        /// <summary>
        /// Отправленно от 
        /// </summary>
        public long FromId
        {
            get { return this.fromId; }
            set { this.fromId = value; OnPropertyChanged(nameof(this.FromId)); }
        }
        /// <summary>
        /// Дата добавления
        /// </summary>
        public DateTime CreateDate
        {
            get { return this.createDate; }
            set { this.createDate = value;OnPropertyChanged(nameof(this.CreateDate)); }
        }
        /// <summary>
        /// Время добавления
        /// </summary>
        public string CreateTime
        {
            get { return this.createTime; }
            set { this.createTime = value; OnPropertyChanged(nameof(this.CreateTime)); }
        }
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public string MessageType
        {
            get { return this.messageType; }
            set { this.messageType = value; OnPropertyChanged(nameof(this.MessageType)); }
        }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string MessageText
        {
            get { return this.messageText; }
            set { this.messageText = value; OnPropertyChanged(nameof(this.MessageText)); }
        }
        /// <summary>
        /// Подпись к файлу
        /// </summary>
        public string MessageCaption
        {
            get { return this.messageCaption; }
            set { this.messageCaption = value; OnPropertyChanged(nameof(this.MessageCaption)); }
        }
        /// <summary>
        /// Название файла
        /// </summary>
        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; OnPropertyChanged(nameof(this.FileName)); }
        }

        /// <summary>
        /// Поле - ИД
        /// </summary>
        private int id;
        /// <summary>
        /// Поле - Ид пользователя
        /// </summary>
        private long userId;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        private string name;
        /// <summary>
        /// Поле - Дата добавления
        /// </summary>
        private long fromId;
        private DateTime createDate;
        /// <summary>
        /// Поле - Время добавления
        /// </summary>
        private string createTime;
        /// <summary>
        /// Поле - Тип сообщения
        /// </summary>
        private string messageType;
        /// <summary>
        /// Поле - Название файла
        /// </summary>
        private string fileName;
        /// <summary>
        /// Поле - Текст сообщения
        /// </summary>
        private string messageText;
        /// <summary>
        /// Поле - подпись к файлу
        /// </summary>
        private string messageCaption;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prog = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prog));
        }
    }
}
