using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CustomersData.Model;
using Newtonsoft.Json;

namespace AccountData.Model
{
    public class Credits : Accounts, INotifyPropertyChanged
    {
        public static event Action<Credits> CreateItems;

        /// <summary>
        /// Конструктор - кредиты
        /// </summary>
        /// <param name="Id">ИД</param>
        /// <param name="AmountIssue">Сумма выданного кредита</param>
        /// <param name="RepayALoan">Осталось погасить</param>
        /// <param name="CreditDate">Дата выдачи кредита</param>
        /// <param name="NextDate">Следующая дата погашения</param>
        /// <param name="MonthsPeriod">Период в месяцах</param>
        /// <param name="Rate">Ставка</param>
        /// <param name="MonthlyPayment">Ежемесячный платеж</param>
        public Credits(int Id, long AccountNumber, decimal Cash, decimal RepayALoan, decimal AmountIssue, int ClientId, int Rate,
            decimal MonthlyPayment, bool State, int MonthsPeriod, DateTime OpenDate, DateTime NextDate, int TypeId)
        {
            this.Id = Id;
            this.AccountNumber = AccountNumber;
            this.Cash = Cash;
            this.RepayALoan = RepayALoan;
            this.AmountIssue = AmountIssue;
            this.ClientId = ClientId;
            this.Rate = Rate;
            this.MonthlyPayment = MonthlyPayment;
            this.State = State;
            this.MonthsPeriod = MonthsPeriod;
            this.OpenDate = OpenDate;
            this.NextDate = NextDate;
            this.TypeId = TypeId;
        }

        public Credits(Credits credits)
        {
            this.Id = credits.Id;
            this.AccountNumber = credits.AccountNumber;
            this.Cash = credits.Cash;
            this.RepayALoan = credits.RepayALoan;
            this.AmountIssue = credits.AmountIssue;
            this.ClientId = credits.ClientId;
            this.Rate = credits.Rate;
            this.MonthlyPayment = credits.MonthlyPayment;
            this.State = credits.State;
            this.MonthsPeriod = credits.MonthsPeriod;
            this.OpenDate = credits.OpenDate;
            this.NextDate = credits.NextDate;
            this.TypeId = credits.TypeId;
        }

        public Credits()
        {
        }

        public Credits(int ClientId)
        {
            this.ClientId = ClientId;
        }

        public void Create(Credits credits)
        {
            CreateItems?.Invoke(credits);
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
        /// Остаток на счету
        /// </summary>
        public override decimal Cash
        {
            get { return this.cash; }
            set { this.cash = value; OnPropertyChanged(nameof(this.Cash)); }
        }
        /// <summary>
        /// Осталось погасить
        /// </summary>
        public decimal RepayALoan
        {
            get { return this.repayaloan; }
            set { this.repayaloan = value; OnPropertyChanged(nameof(this.RepayALoan)); }
        }
        /// <summary>
        /// Поле - выданная сумма
        /// </summary>
        public decimal AmountIssue
        {
            get { return this.amountissue; }
            set { this.amountissue = value; OnPropertyChanged(nameof(this.amountissue)); }
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
        /// Поле - ставка %
        /// </summary>
        public int Rate
        {
            get { return this.rate; }
            set { this.rate = value; OnPropertyChanged(nameof(this.Rate)); }
        }
        /// <summary>
        /// Поле - Ежемесячный платеж
        /// </summary>
        public decimal MonthlyPayment
        {
            get { return this.monthlypayment; }
            set { this.monthlypayment = value; OnPropertyChanged(nameof(this.MonthlyPayment)); }
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
        /// Поле - Период в месяцах
        /// </summary>
        public int MonthsPeriod
        {
            get { return this.monthsperiod; }
            set { this.monthsperiod = value; OnPropertyChanged(nameof(this.MonthsPeriod)); }
        }
        /// <summary>
        /// Дата открытия счета
        /// </summary>
        public override DateTime OpenDate
        {
            get { return this.opendate; }
            set { this.opendate = value; OnPropertyChanged(nameof(this.OpenDate)); }
        }
        /// <summary>
        /// Поле - следующая дата погашения
        /// </summary>
        public DateTime NextDate
        {
            get { return this.nextdate; }
            set { this.nextdate = value; OnPropertyChanged(nameof(this.NextDate)); }
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
        /// ИД -Тип счета
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
        public override int ClientStatus {  get => CustomersData.Repository.GetCustomersId(clientid).StatusId; }

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

        /// <summary>
        /// ИД
        /// </summary>
        private int id;
        /// <summary>
        /// Номер счета
        /// </summary>
        private long accountnumber;
        /// <summary>
        /// Остаток на счету
        /// </summary>
        private decimal cash;
        /// <summary>
        /// Выданная сумма
        /// </summary>
        private decimal amountissue;
        /// <summary>
        /// Осталось погасить
        /// </summary>
        private decimal repayaloan;
        /// <summary>
        /// Ежемесячный платеж
        /// </summary>
        private decimal monthlypayment;
        /// <summary>
        /// Ставка
        /// </summary>
        private int rate;
        /// <summary>
        /// ИД клиента
        /// </summary>
        private int clientid;
        /// <summary>
        /// Состояние счета
        /// </summary>
        private bool state;
        /// <summary>
        /// Клиент
        /// </summary>
        private string clientname;
        /// <summary>
        /// Дата выдачи кредита
        /// </summary>
        private DateTime opendate;
        /// <summary>
        /// Следующая дата погашения
        /// </summary>
        private DateTime nextdate;
        /// <summary>
        /// Период в месяцах
        /// </summary>
        private int monthsperiod;
        /// <summary>
        /// Ид Тип счет
        /// </summary>
        private int typeid;

       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
