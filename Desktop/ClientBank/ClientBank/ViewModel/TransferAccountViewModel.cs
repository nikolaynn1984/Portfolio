
using AccountData;
using AccountData.Model;
using ClientBank.Helper;

using ClientBank.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientBank.ViewModel
{
    public class TransferAccountViewModel : INotifyPropertyChanged
    {
        Repository repository;
        private RelayCommand transferMoney;
        private RelayCommand exit;
        public Accounts GetAccounts { get; set; }
        private int accountId;
        private decimal money;
        private decimal accountMoney;
        private List<Accounts> getListAccounts;

        public TransferAccountViewModel(Accounts accounts)
        {
            repository = App.RepositoryAccount;
            this.GetAccounts = accounts;
            this.AccountId = accounts.Id;
            GetListAccounts = repository.GetListAccounts().ToList();
            ListAccountHandler();
        }

        private void ListAccountHandler()
        {
            var item = GetListAccounts.Where(s => s.Id != AccountId).Where(t => t.TypeId == 2).Where(c => c.State);
            if (GetAccounts.TypeId == 2)
            {
                AccountMoney = GetAccounts.Cash;
            }
            else
            {

                item = GetListAccounts.Where(s => s.Id != AccountId).Where(t => t.TypeId == 2).Where(c => c.ClientId == GetAccounts.ClientId).Where(c => c.State);
                MessageText = $"Только на счета текущего клиента";
                if(GetAccounts.TypeId == 3)
                {
                    bool result = DepositDateChecked();
                    if (result) { AccountMoney = Repository.GetAccountsId(GetAccounts.Id).Cash; GetAccounts.Cash = AccountMoney; }
                    else AccountMoney = GetAccounts.Cash;
                }
                else AccountMoney = GetAccounts.Cash;
            }
            GetListAccounts = new List<Accounts>(item);
        }
        /// <summary>
        /// Проверка дат закрытия вклада
        /// </summary>
        /// <returns></returns>
        private bool DepositDateChecked()
        {
            bool result = true;
            DateTime datenow = DateTime.Now;
            var item = (Deposit)Repository.GetAccountsId(GetAccounts.Id);
            DateTime dateclose = item.DateClose;
            if (datenow < dateclose) result = false;
            return result;
        }
        public decimal AccountMoney
        {
            get { return this.accountMoney; }
            set { this.accountMoney = value; OnPropertyChanged(nameof(this.AccountMoney)); }
        }
       
        public RelayCommand TransferMoney
        {
            get
            {
                return transferMoney ?? (transferMoney = new RelayCommand((o) =>
                {
                    var item = o as TransferAccountView;
                    decimal txtcash = repository.ValidDecimal(item.txtCashTransfer.Text);
                    if(txtcash <= AccountMoney)
                    {
                        if (SelectedAccount != 0)
                        {
                            if (txtcash > 0)
                            {
                                if (GetAccounts.TypeId != 3) { GetAccounts.Cash = AccountMoney; item.DialogResult = true; }
                                else
                                {
                                    bool result = DepositDateChecked();
                                    if (!result)
                                    {
                                        bool dataresult = BankMessageBox.MessYesNo($"Вклад закрывается раньше срока договора. Все проценты по вкладу будут утеряны \nХотите закрыть вклад?");
                                        if (dataresult) { GetAccounts.Cash = AccountMoney; item.DialogResult = true; }
                                        else item.Close();
                                    }
                                    else { GetAccounts.Cash = AccountMoney; item.DialogResult = true; }
                                }


                            }
                            else Money = 0;
                        }
                        else MessageBox.Show("Требуется выбрать счет получателя");
                       
                    }else MessageBox.Show($"Сумма перевода не может превышать остаток - {AccountMoney}");
                            
                }));
            }
        }
        /// <summary>
        /// Отмена перевода - выход
        /// </summary>
        public RelayCommand Exit
        {
            get
            {
                return exit ?? (exit = new RelayCommand((o) =>
                {
                    var item = o as Window;
                    item.Close();
                }));
            }
        }
        public int AccountId
        {
            get { return this.accountId; }
            set { this.accountId = value; OnPropertyChanged(nameof(this.AccountId)); }
        }
        private int selectedaccount;
        public int SelectedAccount
        {
            get { return this.selectedaccount; }
            set { this.selectedaccount = value; OnPropertyChanged(nameof(this.SelectedAccount)); }
        }
        public decimal Money
        {
            get { return this.money; }
            set { this.money = value; OnPropertyChanged(nameof(this.Money)); }
        }
        private string messagetext;
        public string MessageText
        {
            get { return this.messagetext; }
            set { this.messagetext = value; OnPropertyChanged(nameof(this.messagetext)); }
        }
        public List<Accounts> GetListAccounts
        {
            get { return this.getListAccounts; }
            set { this.getListAccounts = value; OnPropertyChanged(nameof(this.GetListAccounts)); }
        }

       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
