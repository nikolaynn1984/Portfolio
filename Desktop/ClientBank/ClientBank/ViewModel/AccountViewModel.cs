using ClientBank.Helper;
using System;
using System.Windows;
using System.Windows.Data;
using AccountData.History;
using CustomersData.Model;
using AccountData.Model;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Reflection;
using System.ComponentModel;
using System.Threading;
using ClientBank.View;
using System.Collections.Generic;
using StoreDatabase;
using ExportData;


namespace ClientBank.ViewModel
{
    public class AccountViewModel : AccountHelper
    {
        public AccountViewModel()
        {
            Loading();

            
        }
        
        /// <summary>
        /// Загрузка
        /// </summary>
        private  void Loading()
        {
            this.customers = App.RepositoryCustom;
            this.account = App.RepositoryAccount;
            ErrorMes.ErrorSqlMessage += ErrorMes_ErrorSqlMessage;
            this.hIstory = new HIstoryRepository();
            StatusFilter = 1;
            
            GetStatuses = CustomersData.Repository.GetStatusesAll();

            CheckFilter = false;
            SearchText = String.Empty;
            ClientRedVisibility = Visibility.Collapsed;
            AccountsList = account.GetListAccounts();
            TypeFilter = 1;
            viewAccount = (ListCollectionView)CollectionViewSource.GetDefaultView(AccountsList);
            viewAccount.CurrentChanged += AccountChanged;
            ClientsList = App.RepositoryCustom.GetCustomers();
            viewClient = (ListCollectionView)CollectionViewSource.GetDefaultView(ClientsList);
            viewClient.CurrentChanged += ClienList_Changed;
            if (viewAccount != null) viewAccount.CurrentChanged += AccountChanged;
            Personal.CreateItems += Personal_Changed;
            Credits.CreateItems += Credits_Changed;
            Deposit.CreateItems += Deposit_Changed;
            OperationHistory.CreateItems += History_Changed;
            ProgressBar.ChangedValue += ProgressBar_ChangedValue;
            FilterHandler += AccountViewModel_FilterHandler;
            AccountsList.CollectionChanged += AccountsList_CollectionChanged;
            ErroreVisible = Visibility.Collapsed;
           
        }

        private void ErrorMes_ErrorSqlMessage(string obj)
        {
            ErrorMessage = obj;
            ErroreVisible = Visibility.Visible;
            Thread.Sleep(10000);
            ErroreVisible = Visibility.Collapsed;
            ErrorMessage = null;
        }

        private void AccountsList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)  {  viewAccount.Refresh();  }

        private void ProgressBar_ChangedValue(ProgressBar obj)
        {
            ProgressBarItem = obj;
            
            CloseWindowMain = ProgressBarItem.ButtonState;


        }

        private void History_Changed(OperationHistory obj) { hIstory.AddHistory(obj); }
        private void Deposit_Changed(Deposit obj) { DepositItems = obj;  Task.Factory.StartNew(AddItem, obj); }
        private void Credits_Changed(Credits obj) {  CreditsItems = obj; Task.Factory.StartNew(AddItem, obj); }
        private void Personal_Changed(Personal obj) { PersonalItems = obj;  Task.Factory.StartNew(AddItem, obj); }
        /// <summary>
        /// Добавление нового счета
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        private void AddItem<T>(T model)
        {
                account.AddItems(model);
        }

        private void AccountChanged(object sender, EventArgs e)
        {
            AccountPosition = $"Запись {viewAccount.CurrentPosition + 1} из {viewAccount.Count}";
            if (viewAccount.CurrentPosition > 0) EnabledPrevious = true;
            else EnabledPrevious = false;
            if (viewAccount.CurrentPosition < viewAccount.Count - 1) EnabledNext = true;
            else EnabledNext = false;
            AccountsItem = viewAccount.CurrentItem as Accounts;
            ContextMenuVisible();
        }

        /// <summary>
        /// Изменения в списке клиенты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClienList_Changed(object sender, EventArgs e)
        {
            var item = viewClient.CurrentItem as Customers;
            if (item != null) ClientFilter = item.Id;
        }
      
        /// <summary>
        /// Обработчик списков данных - Клименты и счета
        /// </summary>
        private void AccountViewModel_FilterHandler()
        {
            if(viewClient != null) viewClient.Filter = new Predicate<object>(CustomersFilter);
            if(viewAccount != null) viewAccount.Filter = new Predicate<object>(AccountsFilterView);
            if (CheckFilter) { FilterButtonText = "Фильт счетов по клиентам включен"; BorderChekedColor = "#52D468"; }
            else { FilterButtonText = "Включить фильтр счетов по клиентам"; BorderChekedColor = "#c8c8c8"; }
        }
        /// <summary>
        /// Обработка отображения контекстнго меню счетов
        /// </summary>
        private void ContextMenuVisible()
        {
            if (AccountsItem != null)
            {
                if (AccountsItem.TypeId == 2)
                {
                    ReplenishButtonVisible = Visibility.Visible;
                    TransferButtonVisible = Visibility.Visible;
                    CloseButtonVisible = Visibility.Visible;
                    MonthlyButtonVisible = Visibility.Collapsed;
                }
                else if (AccountsItem.TypeId == 3)
                {
                    ReplenishButtonVisible = Visibility.Collapsed;
                    TransferButtonVisible = Visibility.Collapsed;
                    CloseButtonVisible = Visibility.Visible;
                    MonthlyButtonVisible = Visibility.Collapsed;
                }
                else
                {
                    ReplenishButtonVisible = Visibility.Collapsed;
                    TransferButtonVisible = Visibility.Visible;
                    CloseButtonVisible = Visibility.Collapsed;
                    MonthlyButtonVisible = Visibility.Visible;
                }

            }
            else
            {
                ReplenishButtonVisible = Visibility.Collapsed;
                TransferButtonVisible = Visibility.Collapsed;
                CloseButtonVisible = Visibility.Collapsed;
                MonthlyButtonVisible = Visibility.Collapsed;
            }
        }

      
        /// <summary>
        /// Фильтр списка клиентов
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CustomersFilter(object obj)
        {
            string text = String.Empty;
             text = SearchText.ToLower();
                Customers items = (Customers)obj;
                if (StatusFilter == 1)
                {
                    if (SearchText != null) return items.ClientName.ToLower().Contains(text);
                    else viewClient.Filter = null;
                }
                else
                {
                    if (SearchText != null) return items.ClientName.ToLower().Contains(text) & items.StatusId == StatusFilter;
                    else return items.StatusId == StatusFilter;
                }

            
            
                return true;
        }
        /// <summary>
        /// Фильтр списка счетов
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool AccountsFilterView(object obj)
        {
            Accounts items = (Accounts)obj;

            if (!CheckFilter)
            {
                if (StatusFilter != 1 & TypeFilter == 1) return items.ClientStatus == StatusFilter;
                else if (StatusFilter == 1 & TypeFilter != 1) return items.TypeId == TypeFilter;
                else if (StatusFilter != 1 & TypeFilter != 1) return items.TypeId == TypeFilter & items.ClientStatus == StatusFilter;
                else viewAccount.Filter = null;
            }
            else
            {
                if (StatusFilter != 1 && TypeFilter == 1 && ClientFilter == 0) return items.ClientStatus == StatusFilter;
                else if (StatusFilter == 1 && TypeFilter != 1 && ClientFilter == 0) return items.TypeId == TypeFilter;
                else if (StatusFilter == 1 && TypeFilter == 1 && ClientFilter != 0) return items.ClientId == ClientFilter;
                else if (StatusFilter != 1 && TypeFilter != 1 && ClientFilter == 0) return items.TypeId == TypeFilter & items.ClientStatus == StatusFilter;
                else if (StatusFilter == 1 && TypeFilter != 1 && ClientFilter != 0) return items.TypeId == TypeFilter & items.ClientId == ClientFilter;
                else if (StatusFilter != 1 && TypeFilter == 1 && ClientFilter != 0) return items.ClientStatus == StatusFilter & items.ClientId == ClientFilter;
                else if (StatusFilter != 1 && TypeFilter != 1 && ClientFilter != 0) return items.ClientStatus == StatusFilter & items.ClientId == ClientFilter & items.TypeId == TypeFilter;
                else viewAccount.Filter = null;
            }

            viewAccount.Refresh();

            return true;
        }
        /// <summary>
        /// Разрешение закрытия главного окна
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal static CancelEventArgs CloseWindow(object e)
        {
            CancelEventArgs window = (CancelEventArgs)e;
            window.Cancel = true;
            if (!CloseWindowMain) MessageBox.Show("Дождитесь завершения операции");
            else 
            {
                var result = BankMessageBox.MessYesNo("Вы уверены что хотите закрыть окно?");
                if (result) { Application.Current.Shutdown(); window.Cancel = false; } 

            }
            return window;
        }
        public static void AccountControlTab_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = (System.Windows.Controls.TabControl)sender;
            int number = item.SelectedIndex;
            TypeFilter = number + 1;
        }

        public RelayCommand ExportToXlsx
        {
            get 
            {
                return exportToXlsx ?? (exportToXlsx = new RelayCommand((o) =>
                {
                    KeeperXlsx keeperXlsx = new KeeperXlsx( StatusFilter);
                    BookExporter book = new BookExporter(keeperXlsx);

                    book.Save();
                }));
            }
        }

        public RelayCommand ExportToPdf
        {
            get
            {
                return exportToPdf ?? (exportToPdf = new RelayCommand((o) =>
                {
                    KeeperPDF keeperPDF = new KeeperPDF(StatusFilter);
                    BookExporter book = new BookExporter(keeperPDF);
                    book.Save();
                }));
            }
        }

        /// <summary>
        /// Команда переключения к следующей записи
        /// </summary>
        public RelayCommand NextCommand
        {
            get
            {
                return nextCommand ?? (nextCommand = new RelayCommand((selecteditem) =>
                {
                    if (selecteditem == null) return;
                    viewAccount.MoveCurrentToNext();
                }));
            }
        }
        /// <summary>
        /// Команда переключения к предыдущей записи
        /// </summary>
        public RelayCommand PreviousCommand
        {
            get
            {
                return previousCommand ?? (previousCommand = new RelayCommand((selecteditem) =>
                {
                    if (selecteditem == null) return;
                    viewAccount.MoveCurrentToPrevious();
                }));
            }
        }
        /// <summary>
        /// Команда переключения к первой записи
        /// </summary>
        public RelayCommand FirstCommand
        {
            get
            {
                return firstCommand ?? (firstCommand = new RelayCommand((selecteditem) =>
                {
                    if (selecteditem == null) return;
                    viewAccount.MoveCurrentToFirst();
                }));
            }
        }
        /// <summary>
        /// Команда переключения к первой записи
        /// </summary>
        public RelayCommand LastCommand
        {
            get
            {
                return lastCommand ?? (lastCommand = new RelayCommand((selecteditem) =>
                {
                    if (selecteditem == null) return;
                    viewAccount.MoveCurrentToLast();
                }));
            }
        }

    }
}
