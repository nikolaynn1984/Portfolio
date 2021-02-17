using ClientBank.Helper;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using CustomersData.Model;
using AccountData.Model;
using AccountData;
using System.Collections.Generic;

namespace ClientBank.ViewModel
{
    class AddDepositViewModel : INotifyPropertyChanged
    {
        private List<Customers> GetCustomers;
        private List<Statuses> GetStatuses;
        private Customers CustomersItem;
        private RelayCommand addaccount;
        private RelayCommand exitEdit;
        public AddDepositViewModel(Deposit deposit)
        {

            this.GetCustomers = new List<Customers>();
            this.GetStatuses = CustomersData.Repository.GetStatusesAll();
            deposit.State = true;
            deposit.OpenDate = DateTime.Now;
            this.CustomersItem = CustomersData.Repository.GetCustomersId(deposit.ClientId); 
            deposit.Rate = GetStatuses.Single(s => s.Id == CustomersItem.StatusId).DepositRate;
            ClientName = CustomersItem.ClientName;
            this.deposititem = deposit;
            StatusClient = CustomersItem.StatusName;
            MonthCount = 3;
            Cash = 50000;
            DatahHandle();
        }

        private void DatahHandle()
        {
            if (MonthCount == 3) MonthhMess = " - Месяца";
            else MonthhMess = " - Месяцев";
            
            DepositlItem.MonthsPeriod = this.MonthCount;
            DepositlItem.Capitalization = this.Capitalization;
            DepositlItem.DepositAmount = this.Cash;
            DepositlItem.Cash = this.Cash;
            DepositlItem.MoneyEarned = this.Incom + this.Cash;
            DepositlItem.DateClose = DateTime.Now.AddMonths(MonthCount);
            Incom = Interest.DepositeRate(DepositlItem);
        }
       
        /// <summary>
        /// Создание счета
        /// </summary>
        public RelayCommand AddAccount
        {
            get
            {
                DatahHandle();
                return addaccount ?? (addaccount = new RelayCommand((o) =>
                {
                    DatahHandle();
                    var item = o as Window;
                    deposititem.Create(DepositlItem);

                    item.DialogResult = true;

                }));
            }
        }
        /// <summary>
        /// Закрыть окно
        /// </summary>
        public RelayCommand ExitEdit
        {
            get
            {
                return exitEdit ?? (exitEdit = new RelayCommand((o) =>
                {
                    var item = o as Window;
                    item.Close();
                }));
            }
        }

        public string ClientName { get; set; }
        public string StatusClient { get; set; }
        private decimal cash;
        public decimal Cash
        {
            get { return this.cash; }
            set { this.cash = value; OnPropertyChanged(nameof(this.Cash)); DatahHandle(); }
        }
        private int monthcount;
        public int MonthCount
        {
            get { return this.monthcount; }
            set { this.monthcount = value; OnPropertyChanged(nameof(this.MonthCount));  DatahHandle(); }
        }
        private decimal incom;
        public decimal Incom
        {
            get { return this.incom; }
            set { this.incom = value; OnPropertyChanged(nameof(this.Incom)); }
        }
      
        private bool capitalization;
        public bool Capitalization
        {
            get { return this.capitalization; }
            set { this.capitalization = value; OnPropertyChanged(nameof(this.Capitalization)); DatahHandle(); }
        }
        private string monthhmess;
        public string MonthhMess
        {
            get { return this.monthhmess; }
            set { this.monthhmess = value; OnPropertyChanged(nameof(this.MonthhMess)); }
        }
        private Deposit deposititem;
        public Deposit DepositlItem
        {
            get { return this.deposititem; }
            set { this.deposititem = value; OnPropertyChanged(nameof(this.DepositlItem)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
