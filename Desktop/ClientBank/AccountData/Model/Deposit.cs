using CustomersData.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AccountData.Model
{
    public class Deposit : Accounts, INotifyPropertyChanged
    {
        public static event Action<Deposit> CreateItems;
        /// <summary>
        /// Конструктор список вкладов
        /// </summary>
        /// <param name="Id">ИД</param>
        /// <param name="Capitalization">Капитализация</param>
        /// <param name="DepositAmount">Сумма - вложенно</param>
        /// <param name="MoneyEarned">Заработано</param>
        /// <param name="Rate">Ставка %</param>
        /// <param name="ClientId">ИД Клиента</param>
        /// <param name="MonthsPeriod">Период в месяцев</param>
        /// <param name="DateOpen">Дата открытия</param>
        /// <param name="DateClose">Дата закрытия</param>
        public Deposit(int Id, long AccountNumber, decimal Cash, decimal DepositAmount, int ClientId,
            bool State, int MonthsPeriod, int Rate, DateTime OpenDate, DateTime DateClose, bool Capitalization, int TypeId, decimal MoneyEarned)
        {
            this.Id = Id;
            this.AccountNumber = AccountNumber;
            this.Cash = Cash;
            this.DepositAmount = DepositAmount;
            this.ClientId = ClientId;
            this.State = State;
            this.MonthsPeriod = MonthsPeriod;
            this.Rate = Rate;
            this.OpenDate = OpenDate;
            this.DateClose = DateClose;
            this.Capitalization = Capitalization;
            this.TypeId = TypeId;
            this.MoneyEarned = MoneyEarned;

        }
        public Deposit(Deposit deposit)
        {
            this.Id = deposit.Id;
            this.AccountNumber = deposit.AccountNumber;
            this.Cash = deposit.Cash;
            this.DepositAmount = deposit.DepositAmount;
            this.ClientId = deposit.ClientId;
            this.State = deposit.State;
            this.MonthsPeriod = deposit.MonthsPeriod;
            this.Rate = deposit.Rate;
            this.OpenDate = deposit.OpenDate;
            this.DateClose = deposit.DateClose;
            this.Capitalization = deposit.Capitalization;
            this.TypeId = deposit.TypeId;
            this.MoneyEarned = deposit.MoneyEarned;
        }
       
        public Deposit()
        {

        }

        public Deposit(int ClientId)
        {
            this.ClientId = ClientId;

        }

        public void Create(Deposit deposit)
        {
            CreateItems?.Invoke(deposit);
        }


        /// <summary>
        /// Поле - ИД
        /// </summary>
        public override int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(nameof(this.Id)); }
        }
        /// <summary>
        /// Поле - номер счета
        /// </summary>
        public override long AccountNumber
        {
            get { return this.accountnumber; }
            set { this.accountnumber = value; OnPropertyChanged(nameof(this.AccountNumber)); }
        }
        /// <summary>
        /// Поле - сумма на счете
        /// </summary>
        public override decimal Cash
        {
            get { return this.cash; }
            set { this.cash = value; OnPropertyChanged(nameof(this.Cash)); }
        }
        /// <summary>
        /// Поле - Сумма вклада
        /// </summary>
        public decimal DepositAmount
        {
            get { return this.depositamount; }
            set { this.depositamount = value; OnPropertyChanged(nameof(this.DepositAmount)); }
        }
        /// <summary>
        /// Поле - ИД Клиента
        /// </summary>
        public override int ClientId
        {
            get { return this.clientid; }
            set { this.clientid = value; OnPropertyChanged(nameof(this.ClientId)); }
        }
        /// <summary>
        /// Поле - Состояние(открыт/закрыт)
        /// </summary>
        public override bool State
        {
            get { return this.state; }
            set { this.state = value; OnPropertyChanged(nameof(this.State)); }
        }
        /// <summary>
        /// Поле - Период вклада в месяцах
        /// </summary>
        public int MonthsPeriod
        {
            get { return this.monthsperiod; }
            set { this.monthsperiod = value; OnPropertyChanged(nameof(this.MonthsPeriod)); }
        }
        /// <summary>
        /// Поле - ставка
        /// </summary>
        public int Rate
        {
            get { return this.rate; }
            set { this.rate = value; OnPropertyChanged(nameof(this.Rate)); }
        }
        /// <summary>
        /// Поле - Дата открытия вклада
        /// </summary>
        public override DateTime OpenDate
        {
            get { return this.opendate; }
            set { this.opendate = value; OnPropertyChanged(nameof(this.OpenDate)); }
        }
        /// <summary>
        /// Поле - Дата закрытия вклада
        /// </summary>
        public DateTime DateClose
        {
            get { return this.dateclose; }
            set { this.dateclose = value; OnPropertyChanged(nameof(this.DateClose)); }
        }
        /// <summary>
        /// Капитализация
        /// </summary>
        public bool Capitalization
        {
            get { return this.capitalization; }
            set { this.capitalization = value; OnPropertyChanged(nameof(this.Capitalization)); }
        }
        /// <summary>
        /// Заработано
        /// </summary>
        public decimal MoneyEarned
        {
            get { return this.moneyearned; }
            set { this.moneyearned = value; OnPropertyChanged(nameof(this.MoneyEarned)); }
        }
        /// <summary>
        /// Имя клиента
        /// </summary>
        public override string ClientName
        {
            get
            {
                if (clientid != 0) this.clientname = CustomersData.Repository.GetCustomersId(clientid).ClientName;
                return this.clientname;
            }
        }

        /// <summary>
        /// Состояние 
        /// </summary>
        public override string StateName
        {
            get
            {
                if (this.State) return "Открыт";
                else return "Закрыт";
            }
        }
        /// <summary>
        /// ИД - Тип счета
        /// </summary>
        public override int TypeId
        {
            get { return this.typeid; }
            set { this.typeid = value; OnPropertyChanged(nameof(this.TypeId)); }
        }

        /// <summary>
        /// Название - Тип счета
        /// </summary>
        public override string TypeName { get => Repository.GetTypeNameId(this.TypeId); }
        /// <summary>
        /// ИД - Статус клиента
        /// </summary>
        public override int ClientStatus { get => CustomersData.Repository.GetCustomersId(clientid).StatusId; }


        /// <summary>
        /// Закрыть счета
        /// </summary>
        /// <param name="result">false если закрыть или true если открыть</param>
        public override void Close(bool result)
        {
            this.State = result;
        }
        /// <summary>
        /// Добавить средства
        /// </summary>
        /// <param name="money">Сумма добавления</param>
        public override void Put(decimal money)
        {
            this.Cash += money;
        }
        /// <summary>
        /// Снять средства
        /// </summary>
        /// <param name="money">Сумма снятия</param>
        public override void Take(decimal money)
        {
            this.Cash -= money;
        }

        private int id;
        /// <summary>
        /// Номер счета
        /// </summary>
        private long accountnumber;
        /// <summary>
        /// Период вклада в месяцах
        /// </summary>
        private int monthsperiod;
        /// <summary>
        /// Дата открытия вклада
        /// </summary>
        private DateTime opendate;
        /// <summary>
        /// Дата закрытия вклада
        /// </summary>
        private DateTime dateclose;
        /// <summary>
        /// Остаток средст на счете
        /// </summary>
        private decimal cash;
        /// <summary>
        /// Ставка
        /// </summary>
        private int rate;
        /// <summary>
        /// Сумма вложения
        /// </summary>
        private decimal depositamount;
        /// <summary>
        /// Поле - заработанно
        /// </summary>
        private decimal moneyearned;
        /// <summary>
        /// Поле - капитализация
        /// </summary>
        private bool capitalization;
        /// <summary>
        /// ИД клиента
        /// </summary>
        private int clientid;
        /// <summary>
        /// Клиент
        /// </summary>
        private string clientname;
        /// <summary>
        /// Состояние счета
        /// </summary>
        private bool state;
        /// <summary>
        /// ИД - тип счета
        /// </summary>
        private int typeid;

       

        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
