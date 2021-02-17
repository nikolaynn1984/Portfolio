using ClientBank.Helper;
using CustomersData.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ClientBank.ViewModel
{
    class MessageViewModel : INotifyPropertyChanged
    {

        private Customers CustomersItem; 
        private RelayCommand addaccount;
        private RelayCommand addDeposit;
        private RelayCommand addCredit;
        public  MessageViewModel(int ClienId)
        {

            this.CustomersItem = CustomersData.Repository.GetCustomersId(ClienId);
            ClientName = CustomersItem.ClientName;
            ButtonHandler();
        }

        private void ButtonHandler()
        {
            if (CustomersItem.StatusId == 4) ButtonEnabled = false;
            else ButtonEnabled = true;
        }

        public RelayCommand AddAccount
        {
            get
            {
                return addaccount ?? (addaccount = new RelayCommand((o) =>
                {
                    Selected = 2;
                    var result = o as Window;
                    result.DialogResult = true;

                }));
            }
        }
        public RelayCommand AddDeposit
        {
            get
            {
                return addDeposit ?? (addDeposit = new RelayCommand((o) =>
                {
                    Selected = 3;
                    var result = o as Window;
                    result.DialogResult = true;

                }));
            }
        }
        public RelayCommand AddCredit
        {
            get
            {
                return addCredit ?? (addCredit = new RelayCommand((o) =>
                {
                    Selected = 4;
                    var result = o as Window;
                    result.DialogResult = true;

                }));
            }
        }
        private bool buttonenabled;
        public bool ButtonEnabled
        {
            get { return this.buttonenabled; }
            set { this.buttonenabled = value; OnPropertyChanged(nameof(this.ButtonEnabled));  }
        }
        private int selected;
        public int Selected
        {
            get { return this.selected; }
            set { this.selected = value; OnPropertyChanged(nameof(this.Selected));}
        }
        

        private string clientname;
        public string ClientName
        {
            get { return this.clientname; }
            set { this.clientname = value;OnPropertyChanged(nameof(this.ClientName)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
