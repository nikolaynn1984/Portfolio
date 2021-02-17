
using AccountData.Credit;
using AccountData.Model;
using ClientBank.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using AccountData;

namespace ClientBank.ViewModel
{
    public class RepaymentViewModel : INotifyPropertyChanged
    {
        CreditRepository repository;
        private Accounts getAccounts;
        private Credits creditsitems;
        private OperationHistory history = new OperationHistory();
        private RelayCommand exitcommand;
        private RelayCommand repaymentmoneycommand;
        private List<Repayment> getrepayments;
        public RepaymentViewModel(Accounts accounts)
        {
            repository = new CreditRepository();
            this.GetAccounts = accounts;
            this.AccountId = accounts.Id;
            this.getrepayments = new List<Repayment>();
            this.history = new OperationHistory();
            
            
            CreditsItem = (Credits)Repository.GetAccountsId(accounts.Id);
            GetRepayments = repository.LoadRepayment(CreditsItem);
            DateClose = CreditsItem.OpenDate.AddMonths(CreditsItem.MonthsPeriod);
            history.AccountNumber = GetAccounts.AccountNumber;
            history.OperationDate = DateTime.Now;
            ViewHandler();
            Money = AccountMoney;
        }
       
        public RelayCommand RepaymentMoneyCommand
        {
            get
            {
                return repaymentmoneycommand ?? (repaymentmoneycommand = new RelayCommand((o) =>
                {
                   var result = ValidationResult.ValidResult.IsValid;

                    if (Money < AccountMoney) MessageBox.Show($"Сумма погашения не должна быть меньше {AccountMoney:0.00} ");
                    else
                    {
                        repository.SaveChanged(CreditsItem, Money);
                        history.Create(history);
                        var item = o as Window;
                        item.DialogResult = true;
                    }

                }));
            }
        }
        /// <summary>
        /// Команда закрытия(отмены)
        /// </summary>
        public RelayCommand ExitCommand
        {
            get
            {
                return exitcommand ?? (exitcommand = new RelayCommand((o) =>
                {
                    var item = o as Window;
                    item.Close();
                }));
            }
        }
        private void ViewHandler()
        {
            if (FullRepay) { AccountMoney = CreditsItem.RepayALoan; history.OperationId = 9; }
            else { AccountMoney = CreditsItem.MonthlyPayment; history.OperationId = 8; }
            history.Money = Money;

            
            
        }
        private decimal accountMoney;
        public decimal AccountMoney
        {
            get { return this.accountMoney; }
            set { this.accountMoney = value; OnPropertyChanged(nameof(this.AccountMoney)); }
        }
        private bool savebutton;
        public bool SaveButton
        {
            get { return this.savebutton; }
            set { this.savebutton = value; OnPropertyChanged(nameof(this.SaveButton)); }
        }
        private int accountId;
        public int AccountId
        {
            get { return this.accountId; }
            set { this.accountId = value; OnPropertyChanged(nameof(this.AccountId)); }
        }
        private bool fullrepay;
        public bool FullRepay
        {
            get { return this.fullrepay; }
            set { this.fullrepay = value; OnPropertyChanged(nameof(this.FullRepay)); ViewHandler(); Money = AccountMoney; }
        }
        private decimal money;
        public decimal Money
        {
            get { return this.money; }
            set { this.money = value; OnPropertyChanged(nameof(this.Money)); ViewHandler(); }
        }
        public Accounts GetAccounts
        {
            get { return this.getAccounts; }
            set { this.getAccounts = value; OnPropertyChanged(nameof(this.GetAccounts)); }
        }
       
        private DateTime dateclose;
        public DateTime DateClose
        {
            get { return this.dateclose; }
            set { this.dateclose = value; OnPropertyChanged(nameof(this.DateClose)); }
        }
        public List<Repayment> GetRepayments
        {
            get { return this.getrepayments; }
            set { this.getrepayments = value; OnPropertyChanged(nameof(this.GetRepayments)); }
        }

       
        public Credits CreditsItem
        {
            get { return this.creditsitems; }
            set { this.creditsitems = value; OnPropertyChanged(nameof(this.CreditsItem)); }
        }
      

       public event PropertyChangedEventHandler PropertyChanged;
       public void OnPropertyChanged([CallerMemberName] string prop = "")
       {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
       }
     }
}
