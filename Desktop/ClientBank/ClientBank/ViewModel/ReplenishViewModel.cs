
using AccountData;
using AccountData.Model;
using ClientBank.Helper;
using ClientBank.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace ClientBank.ViewModel
{
    public class ReplenishViewModel : INotifyPropertyChanged
    {
        public Accounts AccountsItemId { get; set; }
        Repository repository;
        private RelayCommand replenishButton;
        private Personal PersonalItem;
        private decimal money;
        public ReplenishViewModel(Accounts accounts)
        {
            this.repository = App.RepositoryAccount;
            AccountsItemId = accounts;

            PersonalItem = (Personal)Repository.GetAccountsId(accounts.Id);
        }
        public decimal Money
        {
            get { return this.money; }
            set { this.money = value; OnPropertyChanged(nameof(this.Money)); }
        }
       
        public RelayCommand ReplenishButton
        {
            get
            {
                return replenishButton ?? (replenishButton = new RelayCommand((o) =>
                {
                    var item = o as ReplenishView;
                    decimal txtcash = repository.ValidDecimal(item.txtCashReplenish.Text);
                    if(txtcash > 0)
                    {
                        if (Money > 0) PersonalItem.Cash = Money;
                        item.DialogResult = true;
                    }                    
                }));
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
