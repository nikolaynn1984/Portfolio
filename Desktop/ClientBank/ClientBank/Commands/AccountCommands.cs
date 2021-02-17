using AccountData.Model;
using ClientBank.Helper;
using ClientBank.View;
using CustomersData.Model;
using System;
using System.Windows;

namespace ClientBank.Commands
{
    public class AccountCommands
    {
        private static RelayCommand opencustomers;
        private static RelayCommand addaccount;
        private static RelayCommand openhistory;
        private static RelayCommand closeaccountcommand;
        private static RelayCommand tramsferMoneycommand;
        private static RelayCommand replenishAccountcommand;
        private static RelayCommand repaymentcommandbutton;
        static  AccountCommands()
        {

        }
        /// <summary>
        /// Открытие окно истории операций
        /// </summary>
        public static RelayCommand OpenHistorys
        {
            get
            {
                return openhistory ?? (openhistory = new RelayCommand((o) => {
                    HistoryView view = new HistoryView(); view.Show();
                }));
            }
        }
        /// <summary>
        /// Окно выбора дейсткий
        /// </summary>
        public static RelayCommand AddAccount => addaccount ?? (addaccount = new RelayCommand(Choise));
        /// <summary>
        /// Открывает окно клиентов
        /// </summary>
        public static RelayCommand OpenCustomers => opencustomers ?? (opencustomers = new RelayCommand(CustomOpen));
        /// <summary>
        /// Обработка кнопки перевода средств
        /// </summary>
        public static RelayCommand TramsferMoneyCommand => tramsferMoneycommand ?? (tramsferMoneycommand = new RelayCommand(TransferMoneyMethod));
        /// <summary>
        /// Команда закрытия счета
        /// </summary>
        public static RelayCommand CloseAccountCommand => closeaccountcommand ?? (closeaccountcommand = new RelayCommand(CloseAccount));
        /// <summary>
        /// Команда пополнение средст
        /// </summary>
        public static RelayCommand ReplenishAccountCommand => replenishAccountcommand ?? (replenishAccountcommand = new RelayCommand(ReplenishAccount));

        /// <summary>
        /// Окно погашения кредита
        /// </summary>
        public static RelayCommand RepaymentCommandButton
        {
            get
            {
                return repaymentcommandbutton ?? (repaymentcommandbutton = new RelayCommand((o) =>
                {
                    Accounts accounts = (Accounts)o;
                    if (accounts != null)
                    {
                        RepaymentView repayment = new RepaymentView(accounts);
                        repayment.ShowDialog();
                    }
                    else MessageBox.Show("Требуется выбрать счет");
                }));
            }
        }
        /// <summary>
        /// Пополнение средств
        /// </summary>
        /// <param name="obj"></param>
        private static void ReplenishAccount(object obj)
        {
            Accounts accounts = (Accounts)obj;
            if (accounts != null)
            {
                if (accounts.State)
                {
                    ReplenishView replenish = new ReplenishView(accounts);
                    if (replenish.ShowDialog() == true)
                    {
                        decimal money = Convert.ToDecimal(replenish.txtCashReplenish.Text);
                        App.RepositoryAccount.ReplanishAccount(accounts.Id, money);

                    }
                }
                else MessageBox.Show("Операции с закрытым счетом не возможны");
            }
            else MessageBox.Show("Требуется выбрать счет");
        }
        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="obj"></param>
        private static void CloseAccount(object obj)
        {
            Accounts accounts = (Accounts)obj;
            if (accounts.Cash == 0)
            {
                if (accounts.State)
                {
                    bool closeresult = BankMessageBox.MessYesNo($"Вы точно хотите закрыть счет клиента \n{accounts.ClientName}?");
                    if (closeresult)  App.RepositoryAccount.CloseAccount(accounts);
                }
                else MessageBox.Show("Счет закрыт");

            }
            else
            {

                while (true)
                {
                    bool transferresult = BankMessageBox.MessYesNo($"Для закрытия счета баланс не должен превышать - 0 - \nХотите перевести остаток на счете?");
                    if (transferresult)
                    {
                        TransferMoneyMethod(obj);
                        if (accounts.Cash != 0) continue;
                        else
                        {
                            App.RepositoryAccount.CloseAccount(accounts);
                            break;
                        }
                    }
                    else break;
                }
            }
        }
        /// <summary>
        /// Перевод средст
        /// </summary>
        /// <param name="obj"></param>
        private static void TransferMoneyMethod(object obj)
        {
            Accounts accounts = (Accounts)obj;
            if (accounts != null)
            {
                if (accounts.State)
                {
                    int from = accounts.Id;
                    TransferAccountView transfer = new TransferAccountView(accounts);
                    if (transfer.ShowDialog() == true)
                    {
                        int toid = Convert.ToInt32(transfer.selectedAccountTo.SelectedValue);
                        decimal money = Convert.ToDecimal(transfer.txtCashTransfer.Text);
                       App.RepositoryAccount.Transfer(from, toid, money);
                    }

                }
                else MessageBox.Show("Операции с закрытым счетом не возможны");
            }
            else MessageBox.Show("Требуется выбрать счет");
        }
        /// <summary>
        /// Открывает окно с клиентами
        /// </summary>
        /// <param name="obj"></param>
        private static void CustomOpen(object obj)
        {
            CustomersView customersView = new CustomersView();
            customersView.ShowDialog();
        }
        /// <summary>
        /// Выбор открытия счета
        /// </summary>
        /// <param name="obj"></param>
        private static void Choise(object obj)
        {
            Customers item = obj as Customers;
            if (item != null)
            {
                MessageView message = new MessageView(item.Id);
                if (message.ShowDialog() == true)
                {
                    int result = Convert.ToInt32(message.txtSelected.Text);

                    if (result == 2) { AddAccountView addAccount = new AddAccountView(new Personal(ClientId: item.Id)); addAccount.ShowDialog(); }
                    else if (result == 3) { AddDepositView addDeposit = new AddDepositView(new Deposit(ClientId: item.Id)); addDeposit.ShowDialog(); }
                    else if (result == 4) { AddCreditView addCredit = new AddCreditView(new Credits(ClientId: item.Id)); addCredit.ShowDialog(); }
                    else MessageBox.Show("Что то пошло не так");

                }
            }
        }

      
    }
}
