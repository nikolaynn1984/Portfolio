using AccountData.Model;
using CustomersData.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using AccountData.History;
using System.Collections.Generic;

namespace ClientBank.Helper
{
    public class AccountHelper : INotifyPropertyChanged
    {
        private ObservableCollection<Customers> clientslist { get; set; }
        private List<Statuses> getStatuses { get; set; }
        private List<AccountType> getaccounttypes { get; set; }
        private ObservableCollection<Accounts> accountslist { get; set; }
        protected static event Action FilterHandler;
        private int statusfilter;
        private string searchtext;
        private static int typefilter;
        private int clientfilter;
        private string accountPosition;
        private Personal personalitems;
        private Deposit deposititems;
        private Credits creditsitems;
        private ProgressBar progressBar;
        private bool enabledNext;
        private bool enabledPrevious;
        private bool checkfilter;
        private string filterbuttontext;
        private string borderChekedColor;
        private Visibility clientredvisibility;
        protected CustomersData.Repository customers;
        protected AccountData.Repository account;
        protected HIstoryRepository hIstory;
        protected RelayCommand nextCommand;
        protected RelayCommand previousCommand;
        protected RelayCommand firstCommand;
        protected RelayCommand lastCommand;
        protected RelayCommand exportToXlsx;
        protected RelayCommand exportToDoc;
        protected RelayCommand exportToPdf;
        private static bool closewindow;

        protected ListCollectionView viewClient;
        protected ListCollectionView viewType;
        protected ListCollectionView viewAccount;
        private Visibility replenishButtonVisible;
        private Visibility transferButtonVisible;
        private Visibility closeButtonVisible;
        private Visibility monthlyButtonVisible;
        private Accounts accountsitem;
        private OperationHistory historyitem;
        private delegate void SearchChanged();

        public ObservableCollection<Accounts> AccountsList
        {
            get { return this.accountslist; }
            set { this.accountslist = value; OnPropertyChanged(nameof(this.AccountsList)); }
        }
        public ProgressBar ProgressBarItem
        {
            get { return this.progressBar; }
            set { this.progressBar = value; OnPropertyChanged(nameof(this.ProgressBarItem)); }
        }
        public string BorderChekedColor
        {
            get { return this.borderChekedColor; }
            set { this.borderChekedColor = value; OnPropertyChanged(nameof(this.BorderChekedColor)); }
        }
        public Accounts AccountsItem
        {
            get { return this.accountsitem; }
            set { this.accountsitem = value; OnPropertyChanged(nameof(this.AccountsItem)); }
        }
        public bool CheckFilter
        {
            get { return this.checkfilter; }
            set { this.checkfilter = value; OnPropertyChanged(nameof(this.CheckFilter)); FilterHandler?.Invoke(); }
        }
        public string FilterButtonText
        {
            get { return this.filterbuttontext; }
            set { this.filterbuttontext = value; OnPropertyChanged(nameof(this.FilterButtonText)); }
        }
        public Personal PersonalItems
        {
            get { return this.personalitems; }
            set { this.personalitems = value; OnPropertyChanged(nameof(this.PersonalItems)); }
        }
        public Deposit DepositItems
        {
            get { return this.deposititems; }
            set { this.deposititems = value; OnPropertyChanged(nameof(this.DepositItems)); }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get { return this.errorMessage; }
            set { this.errorMessage = value; OnPropertyChanged(nameof(this.ErrorMessage)); }
        }
        public Credits CreditsItems
        {
            get { return this.creditsitems; }
            set { this.creditsitems = value; OnPropertyChanged(nameof(this.CreditsItems)); }
        }
        public OperationHistory HistoryItem
        {
            get { return this.historyitem; }
            set { this.historyitem = value; OnPropertyChanged(nameof(this.HistoryItem)); }
        }
        public ObservableCollection<Customers> ClientsList
        {
            get { return this.clientslist; }
            set { clientslist = value; OnPropertyChanged(nameof(this.clientslist)); }
        }
        public List<AccountType> GetAccountTypes
        {
            get { return this.getaccounttypes; }
            set { this.getaccounttypes = value; OnPropertyChanged(nameof(this.GetAccountTypes)); }
        }
        public List<Statuses> GetStatuses
        {
            get { return this.getStatuses; }
            set { this.getStatuses = value;OnPropertyChanged(nameof(this.GetStatuses)); }
        }
        public int ClientFilter
        {
            get { return this.clientfilter; }
            set { this.clientfilter = value; OnPropertyChanged(nameof(this.ClientFilter)); FilterHandler?.Invoke(); }
        }
        private Visibility erroreVisible;
        public Visibility ErroreVisible
        {
            get { return this.erroreVisible; }
            set { this.erroreVisible = value; OnPropertyChanged(nameof(this.ErroreVisible)); }
        }
        public Visibility ReplenishButtonVisible
        {
            get { return this.replenishButtonVisible; }
            set { this.replenishButtonVisible = value; OnPropertyChanged(nameof(this.ReplenishButtonVisible)); }
        }
        public Visibility TransferButtonVisible
        {
            get { return this.transferButtonVisible; }
            set { this.transferButtonVisible = value; OnPropertyChanged(nameof(this.TransferButtonVisible)); }
        }
        public Visibility CloseButtonVisible
        {
            get { return this.closeButtonVisible; }
            set { this.closeButtonVisible = value; OnPropertyChanged(nameof(this.CloseButtonVisible)); }
        }
        public Visibility MonthlyButtonVisible
        {
            get { return this.monthlyButtonVisible; }
            set { this.monthlyButtonVisible = value; OnPropertyChanged(nameof(this.MonthlyButtonVisible)); }
        }
        public Visibility ClientRedVisibility
        {
            get { return this.clientredvisibility; }
            set { this.clientredvisibility = value; OnPropertyChanged(nameof(this.ClientRedVisibility)); }
        }
        public static int TypeFilter
        {
            get { return typefilter; }
            set { typefilter = value;  FilterHandler?.Invoke(); }
        }
        public int StatusFilter
        {
            get { return this.statusfilter; }
            set { this.statusfilter = value; OnPropertyChanged(nameof(this.StatusFilter)); FilterHandler?.Invoke(); }
        }
        private double progresscount;
        public double ProgressCount
        {
            get { return this.progresscount; }
            set { this.progresscount = value; OnPropertyChanged(nameof(this.ProgressCount)); }
        }
        private double progressnow;
        public double ProgressNow
        {
            get { return this.progressnow; }
            set { this.progressnow = value; OnPropertyChanged(nameof(this.ProgressNow)); }
        }
        public string SearchText
        {
            get { return this.searchtext; }
            set {this.searchtext = value;OnPropertyChanged(nameof(this.SearchText)); FilterHandler?.Invoke(); }
        }
        public string AccountPosition
        {
            get { return this.accountPosition; }
            set { this.accountPosition = value; OnPropertyChanged(nameof(this.AccountPosition)); }
        }
        public static bool CloseWindowMain
        {
            get { return closewindow; }
            set { closewindow = value; }
        }
        public bool EnabledNext
        {
            get { return this.enabledNext; }
            set { this.enabledNext = value; OnPropertyChanged(nameof(this.EnabledNext)); }
        }
        public bool EnabledPrevious
        {
            get { return this.enabledPrevious; }
            set { this.enabledPrevious = value; OnPropertyChanged(nameof(this.EnabledPrevious)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
