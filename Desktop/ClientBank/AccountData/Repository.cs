using AccountData.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Diagnostics;
using AccountData.History;
using CustomersData.Model;
using System.Windows.Threading;
using System.IO;
using AccountData.DataWork;

namespace AccountData
{
    public class Repository
    {
        public static ObservableCollection<Accounts> ListAccount;
        internal static ObservableCollection<Personal> GetPersonals;
        internal static ObservableCollection<Deposit> GetDeposits;
        internal static ObservableCollection<Credits> GetCredits;
        internal static List<AccountType> GetTypes;
        

        public Repository()
        {
            GetPersonals = new ObservableCollection<Personal>();
            GetDeposits = new ObservableCollection<Deposit>();
            GetCredits = new ObservableCollection<Credits>();
            GetTypes = new List<AccountType>();
            ListAccount = new ObservableCollection<Accounts>();
            LoadingData.LoadData();
        }

      
       
        public static List<AccountType> GetAccountTypes() { return GetTypes; }

        public ObservableCollection<Accounts> GetNotFromId(int id)
        {
            var items = ListAccount.Where(s => s.Id != id).Where(t => t.TypeId == 2);
            return new ObservableCollection<Accounts>(items);
        }
        public Task LoadAsync() => Task.Run(LoadingData.LoadAccount);

        #region
        ///// <summary>
        ///// Модель данных счета
        ///// </summary>
        ///// <param name="id">ИД счета</param>
        ///// <returns>Данные счета по ид</returns>
        //public static Personal GetPersonalsId(int id)
        //{
        //    try
        //    {
        //        Personal item = GetPersonals.Single(s => s.Id == id);
        //        return item;
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}
        ///// <summary>
        ///// Модель данных вклада
        ///// </summary>
        ///// <param name="id">ИД счета</param>
        ///// <returns>Данные вклада по ид</returns>
        //public static Deposit GetDepositId(int id)
        //{
        //    Deposit item = GetDeposits.Single(s => s.Id == id);
        //    return item;
        //}
        ///// <summary>
        ///// Модель данных кредита
        ///// </summary>
        ///// <param name="id">ИД счета</param>
        ///// <returns>Данные кредита по ид</returns>
        //public static Credits GetCredittId(int id)
        //{
        //    Credits item = GetCredits.Single(s => s.Id == id);
        //    return item;
        //}
        #endregion

        public static string GetTypeNameId(int id)
        {
            string name = GetTypes.Single(s => s.Id == id)?.Types ?? null;
            return name;
        }

        /// <summary>
        /// Загрузка списк счетов
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Accounts> GetListAccounts()  {  return ListAccount; }
        /// <summary>
        /// Добавление нового счета
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">Модель данных</param>
        public void AddItems<T>(T model)
        {

            if (model is Personal)
            {
                AddPersonelAccount(model);

            }
            else if (model is Credits)
            {
                AddCreditAccount(model);
            }
            else if (model is Deposit)
            {
              AddDepositAccount(model);
            }
            else MessageBox.Show("Что то пошло не так - Счет не добавлен");
        }
        /// <summary>
        /// Добавление нового вклада
        /// </summary>
        /// <typeparam name="T">Deposit</typeparam>
        /// <param name="model">Модель данных</param>
        private  void AddDepositAccount<T>(T model)
        {
            try
            {
                Deposit deposit = model as Deposit;
                deposit.Id = IdGenerator();
                deposit.AccountNumber = NumberGenerator();
                deposit.TypeId = 3;
              //  GetDeposits.Add(deposit);
                deposit.AddDeposit();
                OperationHistory operation = new OperationHistory
                {
                    AccountNumber = deposit.AccountNumber,
                    Money = deposit.DepositAmount,
                    OperationId = 5
                };
                operation.Create(operation);
                AddAccount(model);
                 
            }
            catch (Exception e)
            {
                MessageBox.Show($"Добавление вклада {e}");
            }

        }
        /// <summary>
        /// Добавление новго счета
        /// </summary>
        /// <param name="accounts">Модель данных</param>
        private void AddAccount<T>(T model)
        {
            try
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (Action)delegate ()
                 {
                   
                       ListAccount.Add(AccountFactory.GetAccount(model));
                });
            }
            catch (Exception e)
            {
                MessageBox.Show($"Добавление нового счента {e.Message}");
            }

        }
        /// <summary>
        /// Добавление нового кредита
        /// </summary>
        /// <typeparam name="T">Credits</typeparam>
        /// <param name="model">Модель данных</param>
        private void AddCreditAccount<T>(T model)
        {
            try
            {
                Credits credits = model as Credits;
                credits.Id = IdGenerator();
                credits.AccountNumber = NumberGenerator();
                credits.TypeId = 4;
               // GetCredits.Add(credits);
                credits.AddCredits();
                OperationHistory operation = new OperationHistory
                {
                   AccountNumber = credits.AccountNumber,
                   Money = credits.AmountIssue,
                   OperationId = 7
                };
                operation.Create(operation);
                AddAccount(model);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Добавление кредита {e}");
            }


        }
        /// <summary>
        /// Добавление нового счета
        /// </summary>
        /// <typeparam name="T">Personal</typeparam>
        /// <param name="model">Модель данных</param>
        private  void AddPersonelAccount<T>(T model)
        {
            try
            {
                Personal personal = model as Personal;
                personal.Id = IdGenerator();
                personal.AccountNumber = NumberGenerator();
                personal.TypeId = 2;
                personal.AddPersonal();
                OperationHistory operation = new OperationHistory
                {
                    AccountNumber = personal.AccountNumber,
                    Money = personal.Cash,
                    OperationId = 1
                };
                operation.Create(operation);
                AddAccount(model);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Добавление счета {e}");
            }

        }
        /// <summary>
        /// Генератор ИД
        /// </summary>
        /// <returns></returns>
        private int IdGenerator()
        {
            int id = 1;
            if (ListAccount.Count != 0)
            {
                id = ListAccount.OrderBy(s => s.Id).LastOrDefault().Id;
                id += 1;
            }

            return id;
        }
        /// <summary>
        /// Генератор номера счета
        /// </summary>
        /// <returns></returns>
        private long NumberGenerator()
        {
            long number = 9956456901560001;
            if (ListAccount.Count != 0)
            {
                number = ListAccount.OrderBy(s => s.AccountNumber).LastOrDefault().AccountNumber;
                number += 10;
            }

            return number;
        }

        /// <summary>
        /// Метод пополнения счета
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cash"></param>
        public void ReplanishAccount(int Id, decimal cash)
        {
            var accounts = GetAccountsId(Id);
            accounts.Put(cash);

            SaveAccountChanged(accounts);
        }
        /// <summary>
        /// Метод перевода с счета на счет
        /// </summary>
        /// <param name="FromId">С какого счета переводить</param>
        /// <param name="ToId">На какой счет переводить</param>
        /// <param name="cash">Сумма перевода</param>
        public void Transfer(int FromId, int ToId, decimal cash)
        {
            var FromAccount = GetAccountsId(FromId);
            var ToAccount = GetAccountsId(ToId);
            FromAccount.Take(cash);
            ToAccount.Put(cash);

            SaveAccountChanged(FromAccount);
            SaveAccountChanged(ToAccount);
        }

        private static void SaveAccountChanged(Accounts accounts)
        {
            switch (accounts.TypeId)
            {
                case 2: var itemP = (Personal)accounts; itemP.EditPersonal(); break;
                case 3: var itemD = (Deposit)accounts; itemD.EditDeposit(); break;
                case 4: var itemC = (Credits)accounts; itemC.EditCredits(); break;
            }

        }
        /// <summary>
        /// Проверка введенных данных поля сумма счета
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public decimal ValidDecimal(string value)
        {
            string newvalue = value.Replace(".", ",");
            if (!Decimal.TryParse(newvalue, out decimal number))
            {
                MessageBox.Show("Не корректное заполнение поля Сумма");
                number = 0;
            }
            return number;
        }

        /// <summary>
        /// Загрузка счета по ИД
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static Accounts GetAccountsId(int Id)
        {
            var accounts = ListAccount.FirstOrDefault(s => s.Id == Id);
            return accounts;
        }

        public bool CheckAccount(int id)
        {
            bool result = true;
            int count = ListAccount.Where(s => s.ClientId == id).Count();
            if (count != 0) result = false;
            return result;
        }

        /// <summary>
        /// Метод закрытия счета
        /// </summary>
        /// <param name="accounts"></param>
        public void CloseAccount(Accounts accounts)
        {
            var item = ListAccount.SingleOrDefault(s => s.Id == accounts.Id);
            item.Close(false);
            SaveAccountChanged(item);

        }
    }
}
