using MessagesData.LoaderData;
using MessagesData.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MessagesData
{
    public class Repository
    {
        private ObservableCollection<Users> UsersList { get; set; }
        private ObservableCollection<MessagesLog> MessagesList { get; set; }

        public Repository()
        {
            this.UsersList = new ObservableCollection<Users>();
            this.MessagesList = new ObservableCollection<MessagesLog>();
            GetUsers(); GetMessages();
        }
        /// <summary>
        /// Список пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        public ObservableCollection<Users> GetUsers()
        {
            this.UsersList = SerializeData.GetItems(UsersList, Path.Users);
            return UsersList;
        }
        /// <summary>
        /// Список всех сообщений
        /// </summary>
        /// <returns>Список сообщений</returns>
        public ObservableCollection<MessagesLog> GetMessages()
        {
            this.MessagesList = SerializeData.GetItems(MessagesList, Path.MessagesLog);
            return MessagesList;
        }
        /// <summary>
        /// Список сообщений выбранного пользователя
        /// </summary>
        /// <param name="user">UserId</param>
        /// <returns>Список сообщений</returns>
        public ObservableCollection<MessagesLog> GetMessagesUserId(long user)
        {
            var item = MessagesList.Where(s => s.UserId == user);
            return new ObservableCollection<MessagesLog>(item);
        }
        /// <summary>
        /// Добавление нового сообщения
        /// </summary>
        /// <param name="messages">Модель данных MessagesLog</param>
        public void Add(MessagesLog messages)
        {
            try
            {
                if (messages != null)
                {
                    messages.Id = IdMessageGenerator();
                    messages.CreateDate = DateTime.Now;
                    messages.CreateTime = DateTime.Now.ToShortTimeString();
                    MessagesList.Add(messages);

                    SerializeData.SaveItems(MessagesList, Path.MessagesLog);

                    AddUser(messages.UserId, messages.Name);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
           
        }
        /// <summary>
        /// Добавление нового пользователя в список
        /// </summary>
        /// <param name="userid">Идентификатор пользователя</param>
        /// <param name="nick">Ник</param>
        private void AddUser(long userid, string nick)
        {
            bool result = CheckUser(userid);
            if (result)
            {
                Users users = new Users()
                {
                Id = IdUserGenerator(),
                UserId = userid,
                Nick = nick,
                DateAdded = DateTime.Now
                };
            
                UsersList.Add(users);
                SerializeData.SaveItems(UsersList, Path.Users);
            }
        }

        /// <summary>
        /// Генератор Ид списка пользователей
        /// </summary>
        /// <returns>Новый ИД</returns>
        private int IdUserGenerator()
        {
            int id = 1;
            if (UsersList.Count != 0)
            {
                id = UsersList.OrderBy(s => s.Id).LastOrDefault().Id;
                id += 1;
            }
            return id;
        }
        /// <summary>
        /// Генератор Ид списка сообщений
        /// </summary>
        /// <returns>Новый ИД</returns>
        private int IdMessageGenerator()
        {
            int id = 1;
            if (MessagesList.Count != 0)
            {
                id = MessagesList.OrderBy(s => s.Id).LastOrDefault().Id;
                id += 1;
            }
            return id;
        }

        /// <summary>
        /// Проверка присутствия пользователя в списке
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true если пользователь не найден, в противном случае false</returns>
        private bool CheckUser(long? user)
        {
            bool result = false;
            long? count = (from item in UsersList
                           where item.UserId == user
                           select (long?)item.UserId).Count();
            if (count == 0) result = true;
            return result;
        }
    }
}
