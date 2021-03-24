using Modul10DZ.Client;
using Modul10DZ.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telegram.Bot.Types;

namespace Modul10DZ.View
{

    /// <summary>
    /// Логика взаимодействия для BotWindow.xaml
    /// </summary>
    public partial class BotWindow : Window
    {
        
        static TelegramMessageClient client;
        static long UserId;
        private string UserNick;


        public BotWindow()
        {
            InitializeComponent();
            client = new TelegramMessageClient(this);
            listUsers.ItemsSource = client.Users;
            listMessage.ItemsSource = client.Messages;
            client.Messages.CollectionChanged += Messages_CollectionChanged;

        }

        private void Messages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            LoadMessages();
        }

        private  void LoadMessages()
        {
 
                var list = client.Messages.Where(s => s.UserId == UserId).OrderBy(o => o.CreateDate + o.CreateTime).ToList();
                var observableList = new ObservableCollection<Messages>(list);
                listMessage.ItemsSource = observableList; 


            
        }
        private void listUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MessageLog mes = (MessageLog)listUsers.SelectedItem;
            UserId  = mes.UserId;
            UserNick = mes.Nick;
            btnSend.IsEnabled = true;
            LoadMessages();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            client.SendMessage(txtMessage.Text, UserId, UserNick);

            txtMessage.Text = String.Empty;
            
        }

        private void txtMessage_MouseEnter(object sender, MouseEventArgs e)
        {
            if(txtMessage.Text == "Написать сообщение")
            {
                txtMessage.Text = null;
                txtMessage.Foreground = Brushes.Black;
            }
        }

        private void txtMessage_MouseLeave(object sender, MouseEventArgs e)
        {
            if (String.IsNullOrEmpty(txtMessage.Text))
            {
                txtMessage.Text = "Написать сообщение";
                txtMessage.Foreground = Brushes.Gray;
            }
        }

      

       
    }
}
