using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MessagesData.Model
{
    public class Users: INotifyPropertyChanged
    {
        /// <summary>
        /// Констрктор пользователи
        /// </summary>
        /// <param name="Id">Ид</param>
        /// <param name="UserId">ИДентификатор пользователя</param>
        /// <param name="Nick">Ник </param>
        /// <param name="DateAdded">Дата добавления</param>
        public Users(int Id, long UserId, string Nick, DateTime DateAdded)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.Nick = Nick;
            this.DateAdded = DateAdded;
        }
        /// <summary>
        /// Констрктор пользователи
        /// </summary>
        /// <param name="users">Модель данных</param>
        public Users(Users users)
        {
            this.Id = users.Id;
            this.UserId = users.UserId;
            this.Nick = users.Nick;
            this.DateAdded = DateTime.Now;
        }

        public Users()
        {
        }

        /// <summary>
        /// ИД Пользователя
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(nameof(this.Id)); }
        }
        /// <summary>
        /// Пользователь
        /// </summary>
        public long UserId
        {
            get { return this.userId; }
            set { this.userId = value; OnPropertyChanged(nameof(this.UserId)); }
        }
        /// <summary>
        /// Ник пользователя
        /// </summary>
        public string Nick
        {
            get { return this.nick; }
            set { this.nick = value; OnPropertyChanged(nameof(this.Nick)); }
        }
        /// <summary>
        /// Дата добавления
        /// </summary>
        public DateTime DateAdded
        {
            get { return this.dateAdded; }
            set { this.dateAdded = value;OnPropertyChanged(nameof(this.DateAdded)); }
        }
        /// <summary>
        /// ИД Пользователя
        /// </summary>
        private long userId;
        /// <summary>
        /// Поле ИД пользователя
        /// </summary>
        private int id;
        /// <summary>
        /// Поле Ник пользователя
        /// </summary>
        private string nick;
        /// <summary>
        /// Поле Дата добавления
        /// </summary>
        private DateTime dateAdded;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prog = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prog));
        }
    }
}
