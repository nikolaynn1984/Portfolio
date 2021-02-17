using AccountData.Model;
using ClientBank.Helper;
using CustomersData.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using AccountData;
using System.Collections.Generic;

namespace ClientBank.ViewModel
{
    class AddCreditViewModel : INotifyPropertyChanged
    {

        private List<Statuses> GetStatuses;
        private Customers CustomersItem;
        private RelayCommand addaccount;
        private RelayCommand exitEdit;
        private CustomersData.Repository repository;
        public AddCreditViewModel(Credits credits)
        {

            this.GetStatuses = new List<Statuses>();
            this.repository = App.RepositoryCustom;

            GetStatuses = repository.GetStatuses();
            credits.State = true;
            credits.OpenDate = DateTime.Now;
            credits.NextDate = DateTime.Now.AddMonths(1);
            this.CustomersItem = CustomersData.Repository.GetCustomersId(credits.ClientId);
            credits.Rate = GetStatuses.Single(s => s.Id == CustomersItem.StatusId).CreditRate;
            ClientName = CustomersItem.ClientName;
            this.credititem = credits;
            StatusClient = CustomersItem.StatusName;
            MonthCount = 6;
            Cash = 50000;
            DatahHandle();
        }
        private void DatahHandle()
        {
            MonthText();
            CreditItem.MonthsPeriod = this.MonthCount;
            CreditItem.AmountIssue = this.Cash;
            CreditItem.Cash = this.Cash;
            MonthlyPayment = Interest.CreditRate(CreditItem);
            CreditItem.RepayALoan = MonthlyPayment * MonthCount;
            CreditItem.MonthlyPayment = MonthlyPayment;
        }
        private void MonthText()
        {
            int count = 0;
            int year = 0;
            if (MonthCount <= 12)
            {
                if (MonthCount == 12) MonthhMess = $"1 - Год";
                else MonthhMess = $"{MonthCount} - Месяцев";
            }
            else if (MonthCount > 12 & MonthCount < 24)
            {
                count = monthcount - 12;
                if (count == 1) MonthhMess = $"1 - Год, {count} - Месяц";
                else if (count == 2 | count <= 4) MonthhMess = $"1 - Год, {count} - Месяца";
                else MonthhMess = $"1 - Год, {count} - Месяцев";
            }
            else if (MonthCount >= 24 & MonthCount <= 59)
            {
                if (MonthCount <= 24) { count = monthcount - 12; year = 2; }
                else if (MonthCount >= 25 & MonthCount <= 35) { count = MonthCount - 24; year = (MonthCount - count) / 12; }
                else if (MonthCount >= 36 & MonthCount <= 47) { count = MonthCount - 36; year = (MonthCount - count) / 12; }
                else { count = MonthCount - 48; year = (MonthCount - count) / 12; }
                if (count == 1) MonthhMess = $"{year} - Года, {count} - Месяц";
                else if (count == 12) MonthhMess = $"{year} - Года";
                else if (count == 0) MonthhMess = $"{year} - Года";
                else if (count == 2 | count <= 4) MonthhMess = $"{year} - Года, {count} - Месяца";
                else MonthhMess = $"{year} - Года, {count} - Месяцев";
            }
            else MonthhMess = "5 - Лет";

        }
       

        public RelayCommand AddAccount
        {
            get
            {
                return addaccount ?? (addaccount = new RelayCommand((o) =>
                {
                    var item = o as Window;
                    credititem.Create(CreditItem);

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
        private decimal monthlypayment;
        public decimal MonthlyPayment
        {
            get { return this.monthlypayment; }
            set { this.monthlypayment = value; OnPropertyChanged(nameof(this.MonthlyPayment)); }
        }
        private int monthcount;
        public int MonthCount
        {
            get { return this.monthcount; }
            set { this.monthcount = value; OnPropertyChanged(nameof(this.MonthCount)); DatahHandle(); }
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
        private Credits credititem;
        public Credits CreditItem
        {
            get { return this.credititem; }
            set { this.credititem = value; OnPropertyChanged(nameof(this.CreditItem)); }
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
