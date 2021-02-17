using AccountData.Model;
using ClientBank.Helper;
using ClientBank.View;
using CustomersData.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.Generic;

namespace ClientBank.ViewModel
{
    class AddAccountViewModel : INotifyPropertyChanged
    {
        private Customers CustomersItem;
        private RelayCommand addaccount;
        private RelayCommand exitEdit;
        public AddAccountViewModel(Personal personal)
        {

            personal.State = true;
            personal.OpenDate = DateTime.Now;

            this.CustomersItem = CustomersData.Repository.GetCustomersId(personal.ClientId);
            ClientName = CustomersItem.ClientName;
            this.personalitem = personal;
            StatusClient = CustomersItem.StatusName;
        }
        /// <summary>
        /// Создание счета
        /// </summary>
        public RelayCommand AddAccount
        {
            get
            {
                return addaccount ?? (addaccount = new RelayCommand((o) =>
                {
                    var item = o as AddAccountView;
                    personalitem.Create(PersonalItem);
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
       
       
        private Personal personalitem;
        public Personal PersonalItem
        {
            get { return this.personalitem; }
            set { this.personalitem = value; OnPropertyChanged(nameof(this.PersonalItem)); }
        }
      
        


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
