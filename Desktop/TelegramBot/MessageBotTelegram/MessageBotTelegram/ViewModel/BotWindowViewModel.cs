using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TelegramClient;
using MessagesData;
using MessagesData.Model;
using System.Windows.Data;
using MessageBotTelegram.UI;
using System.Windows;
using System.Threading;
using MessageBotTelegram.View;
using System.Windows.Controls;

namespace MessageBotTelegram.ViewModel
{
    public class BotWindowViewModel: INotifyPropertyChanged, IDataErrorInfo
    {
        private Client client;
        static string token = "Ваш токен";//Токен Бота
        private Repository repository;
        private RelayCommand sendmessage;
        public ListCollectionView ViewMessage;
        public ListCollectionView ViewClient;
        public ScrollViewer scrollViewer;
        public MessagesLog MessagesItem;

        public BotWindowViewModel()
        {
            this.client = new Client(token);
            this.repository = new Repository();
            this.MessagesItem = new MessagesLog();
           ClientsList = repository.GetUsers();
            MessagesList = repository.GetMessages();
            
            ViewMessage = (ListCollectionView)CollectionViewSource.GetDefaultView(MessagesList);
            ViewClient = (ListCollectionView)CollectionViewSource.GetDefaultView(ClientsList);
            ViewClient.CurrentChanged += new EventHandler( ViewClient_CurrentChanged);
            ViewMessage.CurrentChanged += new EventHandler(Message_CurrentChanged);
            ViewMessage.Filter = new Predicate<object>(MessageFilter);
            
            MessagesLog.CreateItems += NewMessagesHandler;
        }

        private bool MessageFilter(object obj)
        {
            MessagesLog messages = (MessagesLog)obj;
            if (UserId != 0) return messages.UserId == UserId;

            return true;
        }

        private void Message_CurrentChanged(object sender, EventArgs e)
        {
            int text = ViewMessage.CurrentPosition + 1;
            //ViewMessage.MoveCurrentToLast();
        }

        private void ViewClient_CurrentChanged(object sender, EventArgs e)
        {
            var item = ViewClient.CurrentItem as Users;
            if (item != null)
            {
                UserId = item.UserId;
                UserNick = item.Nick;
                ViewMessage.Refresh();
            }
        }

        private void NewMessagesHandler(MessagesLog obj)
        {
            MessagesItem = obj;
            Thread thread = new Thread(AddItem);
            thread.Start();

        }
        /// <summary>
        /// Добавление сообщения в коллекцию
        /// </summary>
        private void AddItem()
        {
            Application.Current.Dispatcher?.Invoke(()=>{
                repository.Add(MessagesItem);
            }); 
        }
        /// <summary>
        /// Отправка нового сообщения
        /// </summary>
        public RelayCommand SendMessage
        {
            get 
            {
                return sendmessage ?? (sendmessage = new RelayCommand((o) =>
                {
                    FromMyMessage.NewMessage(token, MessageText, UserId, UserNick);
                    MessageText = null;
                }));
            }
        }


        private ObservableCollection<Users> clientslist { get; set; }
        public ObservableCollection<Users> ClientsList
        {
            get { return this.clientslist; }
            set { this.clientslist = value; OnPropertyChanged(nameof(this.ClientsList)); }
        }
        private ObservableCollection<MessagesLog> messageslist { get; set; }
        public ObservableCollection<MessagesLog> MessagesList
        {
            get { return this.messageslist; }
            set { this.messageslist = value; OnPropertyChanged(nameof(this.MessagesList)); }
        }
        private string messagetext;
        public string MessageText
        {
            get { return this.messagetext; }
            set { this.messagetext = value; OnPropertyChanged(nameof(this.MessageText)); }
        }
        private long userid;
        public long UserId
        {
            get { return this.userid; }
            set { this.userid = value; OnPropertyChanged(nameof(this.UserId)); }
        }
        private string usernick;
        public string UserNick
        {
            get { return this.usernick; }
            set { this.usernick = value; OnPropertyChanged(nameof(this.UserNick)); }
        }
        private bool sendbutton;
        public bool SendButton
        {
            get { return this.sendbutton; }
            set { this.sendbutton = value; OnPropertyChanged(nameof(this.SendButton)); }
        }
        public string Error { get { return null; } }
        /// <summary>
        /// Обработка поля отправки сообщения
        /// </summary>
        /// <param name="columnName">Поле</param>
        /// <returns>Результат</returns>
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "MessageText":
                        if (String.IsNullOrEmpty(MessageText))
                        {
                            error = "Поле не должно быть пустым";
                            SendButton = false;
                        }
                        else SendButton = true;
                        break;
                }

                return error;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
