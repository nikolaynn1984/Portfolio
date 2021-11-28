using Landing.Model.Data;
using Landing.Model.Notifity;
using Landing.Repository;
using Landing.Win.Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Landing.Win.ViewModel
{
    public class MessageBotViewModel : BaseNotifiry, IDataErrorInfo
    {
        private readonly RMessageBot data;
        public ListCollectionView ViewMessage;
        public ListCollectionView ViewClient;
        OpenFileDialog dialog = new OpenFileDialog();
        bool connection;
        private string Path = string.Empty;
        public MessageBotViewModel()
        {
            this.data = new RMessageBot(App.url, App.user.Token);
            this.users = new ObservableCollection<MessageUser>();
            AttackVisible = Visibility.Collapsed;
            this.messages = new ObservableCollection<MessagesBot>();
            ViewMessage = (ListCollectionView)CollectionViewSource.GetDefaultView(MessagesList);
            ViewClient = (ListCollectionView)CollectionViewSource.GetDefaultView(UsersList);
            ViewClient.CurrentChanged += new EventHandler(ViewClient_CurrentChanged);
            RMessageBot.MessageBotEvent += MessageBotChanged;
            RMessageBot.MessageConnectionEvent += BotConnectionChanged;
            Task.Run(LoadUsers);
        }

        private RelayCommand sendmessage;
        private RelayCommand attachbtn;
        private RelayCommand sendattach;
        private RelayCommand closeattach;

        public RelayCommand RelaySendMessage => sendmessage ??= new RelayCommand(SendMessage);
        public RelayCommand RelayAttachbtn => attachbtn ??= new RelayCommand(OpenDialog);
        public RelayCommand RelaySendAttach => sendattach ??= new RelayCommand(SendAttachFile);
        public RelayCommand RelayCloseAttach => closeattach ??= new RelayCommand(CancelAttach);

        /// <summary>
        /// Метод отправки сообщения
        /// </summary>
        private async void SendMessage(object obj)
        {
            if (!string.IsNullOrEmpty(MessageText))
            {
                if(UserItem != null)
                {
                    MessagesBot messages = new MessagesBot
                    {
                        Id = 0,
                        MessageText = MessageText,
                        MessageCaption = "",
                        UserMessageId = UserItem.Id,
                        FromIdUser = 0,
                        CreateDate = DateTime.Now,
                        SendType = "To",
                        MessgeType = "Text",
                        ImageId = 0
                    };
                    await data.AddMessage(messages);
                }
               
                 
                MessageText = "";
            }

        }
        /// <summary>
        /// Открываем диалоговое окно выбора изображения
        /// </summary>
        private void OpenDialog(object obj)
        {
            if (UserItem != null)
            {
                dialog.Title = "Выбрать изобрадения";
                dialog.Filter = "Все возможные форматы | *.jpg; *.jpeg; *.png | JPEG *.jpg;*.jpeg | Портативная сетевая графика *.png";
                if (dialog.ShowDialog() == true)
                {
                    Path = dialog.FileName.ToString();
                    if (!string.IsNullOrEmpty(Path))
                    {
                        AttachImage = new BitmapImage(new Uri(dialog.FileName));
                        AttackVisible = Visibility.Visible;
                    }

                }
            }
            else MessageBox.Show("Требуется выбрать пользователя");
           
        }
        /// <summary>
        /// Отправляем изображени
        /// </summary>
        private async void SendAttachFile(object obj)
        {
            if (!string.IsNullOrEmpty(Path))
            {
                MessagesBot messages = new MessagesBot
                {
                    Id = 0,
                    MessageText = "",
                    MessageCaption = !string.IsNullOrEmpty(CaptionText) ? CaptionText : "",
                    UserMessageId = UserItem.Id,
                    FromIdUser = 0,
                    CreateDate = DateTime.Now,
                    SendType = "To",
                    MessgeType = "Photo",
                    ImageId = 0
                };
                await data.AddMessage(messages, Path);
                CaptionText = string.Empty;
                AttackVisible = Visibility.Collapsed;
            }
        }
        /// <summary>
        /// Отмена отправки выбранного изображения
        /// </summary>
        private void CancelAttach(object obj)
        {
            AttackVisible = Visibility.Collapsed;
            Path = string.Empty;
            CaptionText = string.Empty;
        }
        /// <summary>
        /// Изменение пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewClient_CurrentChanged(object sender, EventArgs e)
        {
            var item = ViewClient.CurrentItem as MessageUser;
            if (item != null)
            {
                UserItem = item;
                MessagesList.Clear();
                Task.Run(() => LoadMessages(item.Id));
            }
        }

        private async void LoadMessages(int id)
        {
            
            IEnumerable<MessagesBot> items = await data.GetMessagesByUserId(id);

            Parallel.ForEach(items, item => AddMessage(item));
        }

        /// <summary>
        /// Слушаем изменения соединяния бота
        /// </summary>
        /// <param name="result">Результат, true - если соединение установле, в противном случае false</param>
        private void BotConnectionChanged(bool result)
        {
            CheckConnectionInfo(result);
        }
        /// <summary>
        /// Меняем информацию актуализации соединения телеграмм бота
        /// </summary>
        /// <param name="result"></param>
        private void CheckConnectionInfo(bool result)
        {
            connection = result;
            if (result == true)
            {
                ConColor = "#177245";
                ConString = "Соединение";
            }
            else
            {
                ConColor = "#DC143C";
                ConString = "Нет соединения";
            }
        }
        /// <summary>
        /// Слушаем получение сообщений
        /// </summary>
        /// <param name="mes"></param>
        private void MessageBotChanged(MessagesBot mes)
        {
            Task.Run(() => NewMessageHandler(mes));
        }
        /// <summary>
        /// Добавляем сообщение в список
        /// </summary>
        /// <param name="mes"></param>
        /// <returns></returns>
        private async Task NewMessageHandler(MessagesBot mes)
        {
            var user = UsersList.SingleOrDefault(s => s.Id == mes.UserMessageId);
            if (user == null)
            {
                var newuser = await data.GetUserById(mes.UserMessageId);
                if(newuser.Id != 0) AddUser(newuser);
            }
            if (UserItem.Id == mes.UserMessageId) AddMessage(mes);
        }
        /// <summary>
        /// Загружаем список пользователей
        /// </summary>
        /// <returns></returns>
        private async Task LoadUsers()
        {
            IEnumerable<MessageUser> items = await data.GetUsers();
            bool resul = await data.CheckConnection();
            CheckConnectionInfo(resul);
            Parallel.ForEach(items, item => AddUser(item));
        }
        /// <summary>
        /// Добавляем пользователя в список
        /// </summary>
        /// <param name="item"></param>
        private void AddUser(MessageUser item)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                UsersList.Add(item);
            });
        }
        /// <summary>
        /// Добавить сообщение
        /// </summary>
        /// <param name="item"></param>
        private void AddMessage(MessagesBot item)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessagesList.Add(item);
            });
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
                string error = string.Empty;
                switch (columnName)
                {
                    case "MessageText":
                        if (string.IsNullOrEmpty(MessageText))
                        {
                            error = "Поле не должно быть пустым";
                            SendButton = false;
                        }
                        else
                        {
                            if (UserItem != null)
                            {
                                SendButton = true;
                            }
                        }
                        break;
                }

                return error;
            }
        }

        private bool sendbutton;
        public bool SendButton
        {
            get { return this.sendbutton; }
            set { this.sendbutton = value; OnPropertyChanged(nameof(this.SendButton)); }
        }

        private Visibility attackvisible;
        public Visibility AttackVisible
        {
            get { return this.attackvisible; }
            set { this.attackvisible = value; OnPropertyChanged(nameof(this.AttackVisible)); }
        }

        private BitmapImage attachimage;
        public BitmapImage AttachImage
        {
            get { return this.attachimage; }
            set { this.attachimage = value; OnPropertyChanged(nameof(this.AttachImage)); }
        }

        private ObservableCollection<MessageUser> users;
        private MessageUser useritem;
        public MessageUser UserItem
        {
            get
            {
                return this.useritem;
            }
            set
            {
                this.useritem = value;
                OnPropertyChanged(nameof(this.UserItem));
            }
        }
        public ObservableCollection<MessageUser> UsersList
        {
            get
            {
                return this.users;
            }
            set
            {
                this.users = value;
                OnPropertyChanged(nameof(this.UsersList));
            }
        }

        private ObservableCollection<MessagesBot> messages;
        public ObservableCollection<MessagesBot> MessagesList
        {
            get
            {
                return this.messages;
            }
            set
            {
                this.messages = value;
                OnPropertyChanged(nameof(this.MessagesList));
            }
        }

        private string constring;
        private string concolor;
        private string messagetext;
        private string captiontext;

        public string ConString
        {
            get
            {
                return this.constring;
            }
            set
            {
                this.constring = value;
                OnPropertyChanged(nameof(this.ConString));
            }
        }
        public string ConColor
        {
            get
            {
                return this.concolor;
            }
            set
            {
                this.concolor = value;
                OnPropertyChanged(nameof(this.ConColor));
            }
        }
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
        public string CaptionText
        {
            get
            {
                return this.captiontext;
            }
            set
            {
                this.captiontext = value;
                OnPropertyChanged(nameof(this.MessageText));
            }
        }

    }
}
