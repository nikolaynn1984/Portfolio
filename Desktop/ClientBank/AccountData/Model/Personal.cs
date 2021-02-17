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
    public class Personal : Accounts, INotifyPropertyChanged
    {
        public static event Action<Personal> CreateItems;
        public Personal(){        }
        /// <summary>
        /// Конструктор счетов
        /// </summary>
        /// <param name="Id">ИД</param>
        /// <param name="AccountNumber">Номер счета</param>
        /// <param name="Cash">Сумма</param>
        /// <param name="ClientId">Ид Клиента</param>
        /// <param name="OpenDate">Дата открытия</param>
        /// <param name="State">Состояние</param>
        /// <param name="TypeId">ИД - Тип счета</param>
        public Personal(int Id, long AccountNumber, decimal Cash, int ClientId, DateTime OpenDate, bool State, int TypeId)
        {
            this.Id = Id;
            this.AccountNumber = AccountNumber;
            this.Cash = Cash;
            this.ClientId = ClientId;
            this.OpenDate = OpenDate;
            this.State = State;
            this.TypeId = TypeId;

        }
        public void Create(Personal personal)
        {
            CreateItems?.Invoke(personal);
        }

        public Personal(int ClientId)
        {
            this.ClientId = ClientId;
        }

        public Personal(Personal model)
        {
            this.id = model.Id;
            this.AccountNumber = model.AccountNumber;
            this.Cash = model.Cash;
            this.State = model.State;
            this.ClientId = model.ClientId;
            this.TypeId = model.TypeId;
            this.OpenDate = model.OpenDate;
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
        /// Поле - ИД Клиента
        /// </summary>
        public override int ClientId
        {
            get { return this.clientid; }
            set { this.clientid = value; OnPropertyChanged(nameof(this.ClientId)); }
        }
        public override string ClientName
        {
            get
            {
                if (clientid != 0) this.clientname = CustomersData.Repository.GetCustomersId(clientid).ClientName;
                return this.clientname;
            }
        }
        public override DateTime OpenDate
        {
            get { return this.opendate; }
            set { this.opendate = value; OnPropertyChanged(nameof(this.OpenDate)); }
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


        /// <summary>
        /// ИД
        /// </summary>
        private int id;
        /// <summary>
        /// Номер счета
        /// </summary>
        private long accountnumber;
        /// <summary>
        /// Остаток средст на счете
        /// </summary>
        private decimal cash;
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
        /// Дата открытия счета
        /// </summary>
        private DateTime opendate;
        /// <summary>
        /// Ид - тип счета
        /// </summary>
        private int typeid;

        public override string ToString()
        {
            return $"{Id}\n{AccountNumber} \n{Cash} \n{ClientId} \n{ OpenDate} \n{ State}";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
