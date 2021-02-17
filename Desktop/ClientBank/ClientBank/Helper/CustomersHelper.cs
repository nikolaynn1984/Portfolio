
using CustomersData;
using CustomersData.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace ClientBank.Helper
{
    public class CustomersHelper : INotifyPropertyChanged
    {
        protected Repository repository;
        protected AccountData.Repository repositoryAccount;
        protected ListCollectionView view;
        private string emploeePosition;
        private bool enabledNext;
        private bool enabledPrevious;
        private bool statusenabled;
        private bool vipstatus;
        private Visibility vipclientvisible;


        private Visibility getvisibility;
        protected RelayCommand saveItem;
        protected RelayCommand nextCommand;
        protected RelayCommand additem;
        protected RelayCommand edititem;
        protected RelayCommand exitedit;
        protected RelayCommand removeitem;
        protected RelayCommand previousCommand;
        protected RelayCommand firstCommand;
        protected RelayCommand lastCommand;
        protected RelayCommand vipchangedcommand;
        protected ObservableCollection<Customers> customers;
        protected List<Statuses> statuses { get; set; }
        protected List<Cities> cities { get; set; }
        protected ObservableCollection<PhysicalClient> physicalClients;
        protected ObservableCollection<Busines> businesClient;
        private Customers customersItem;
        private Visibility visibleclient;
        private Visibility visiblebusines;
        private string name;
        private string surname;
        private string fio;
        private string datebirthday;
        private long inn;
        private int statusid;
        private int cityid;
        public int ClientId;
        private bool savebutton;

        public int StatusId
        {
            get
            {
                StatusIdChanged(this.statusid);
                return this.statusid;
            }
            set { this.statusid = value; OnPropertyChanged(nameof(this.StatusId)); }
        }
        public string FIO
        {
            get { return this.fio; }
            set { this.fio = value; OnPropertyChanged(nameof(this.FIO)); }
        }
        public long INN
        {
            get { return this.inn; }
            set { this.inn = value; OnPropertyChanged(nameof(this.INN)); }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; OnPropertyChanged(nameof(this.Name)); }
        }
        public string Surname
        {
            get { return this.surname; }
            set { this.surname = value; OnPropertyChanged(nameof(this.Surname)); }
        }
        public int CityId
        {
            get { return this.cityid; }
            set { this.cityid = value; OnPropertyChanged(nameof(this.CityId)); }
        }
        public bool SaveButton
        {
            get { return this.savebutton; }
            set { this.savebutton = value; OnPropertyChanged(nameof(this.SaveButton)); }
        }
        public bool VipStatus
        {
            get { return this.vipstatus; }
            set { this.vipstatus = value; OnPropertyChanged(nameof(this.VipStatus)); }
        }
        public Visibility VIPClientVisible
        {
            get { return this.vipclientvisible; }
            set { this.vipclientvisible = value; OnPropertyChanged(nameof(this.VIPClientVisible)); }
        }
        public Visibility VisibleClient
        {
            get { return this.visibleclient; }
            set { this.visibleclient = value; OnPropertyChanged("VisibleClient"); }
        }
        public Visibility VisibleBusines
        {
            get { return this.visiblebusines; }
            set { this.visiblebusines = value; OnPropertyChanged("VisibleBusines"); }
        }
        public ObservableCollection<Customers> CustomersItems
        {
            get { return customers; }
            set { customers = value; OnPropertyChanged(nameof(this.CustomersItems)); }
        }
        public string DateBirthday
        {
            get { return datebirthday; }
            set { this.datebirthday = value; OnPropertyChanged(nameof(this.DateBirthday)); }
        }
        private DateTime birthday;
        public DateTime Birthday
        {
            get { return this.birthday; }
            set { this.birthday = value; OnPropertyChanged(nameof(this.Birthday)); }
        }
        public Customers CustomersItem
        {
            get { return customersItem; }
            set { customersItem = value; OnPropertyChanged("CustomersItem"); }
        }
       

        public List<Statuses> StatusList
        {
            get { return statuses; }
            set { statuses = value; OnPropertyChanged("StatusList"); }
        }
        public List<Cities> CitiesList
        {
            get { return cities; }
            set { cities = value; OnPropertyChanged("CitiesList"); }
        }
        public Visibility GetVisibility
        {
            get { return getvisibility; }
            set { getvisibility = value; OnPropertyChanged("GetVisibility"); }
        }
        public bool EnabledNext
        {
            get { return enabledNext; }
            set { enabledNext = value; OnPropertyChanged("EnabledNext"); }
        }
        public bool EnabledPrevious
        {
            get { return enabledPrevious; }
            set { enabledPrevious = value; OnPropertyChanged("EnabledPrevious"); }
        }
        public bool StatusEnabled
        {
            get { return this.statusenabled; }
            set { this.statusenabled = value; OnPropertyChanged(nameof(this.StatusEnabled)); }
        }
        public string EmploeePosition
        {
            get { return emploeePosition; }
            set { emploeePosition = value; OnPropertyChanged("EmploeePosition"); }
        }
        private void StatusIdChanged(int status)
        {
            if (status == 4)
            {
                VisibleBusines = Visibility.Visible;
                VisibleClient = Visibility.Collapsed;

            }
            else
            {
                VisibleBusines = Visibility.Collapsed;
                VisibleClient = Visibility.Visible;
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
