using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AccountData.History;

namespace AccountData.Model
{
    public class OperationHistory : INotifyPropertyChanged
    {
        public static event Action<OperationHistory> CreateItems;



        /// <summary>
        /// История операций
        /// </summary>
        /// <param name="Id">ИД</param>
        /// <param name="OperationId">ИД операции</param>
        /// <param name="Money">Сумма</param>
        /// <param name="OperationDate">Дата операции</param>
        /// <param name="AccountNumber">Номер счета</param>
        public OperationHistory(int Id, int OperationId, decimal Money, DateTime OperationDate, long AccountNumber)
        {
            this.Id = Id;
            this.OperationId = OperationId;
            this.Money = Money;
            this.OperationDate = OperationDate;
            this.AccountNumber = AccountNumber;

        }

        public void Create(OperationHistory history)
        {
            history.OperationDate = DateTime.Now;
            CreateItems?.Invoke(history);
        }

        public OperationHistory()
        {

        }

        /// <summary>
        /// Поле - ИД
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(nameof(this.id)); }
        }
        /// <summary>
        /// Поле - ИД операции
        /// </summary>
        public int OperationId
        {
            get { return this.operationid; }
            set { this.operationid = value; OnPropertyChanged(nameof(this.operationid)); }
        }
        /// <summary>
        /// Поле - Название операции
        /// </summary>
        public string OperationName
        {
            get { return HIstoryRepository.GetOperationNameId(this.operationid); }
        }
        /// <summary>
        /// Поле - Сумма
        /// </summary>
        public decimal Money
        {
            get { return this.money; }
            set { this.money = value; OnPropertyChanged(nameof(this.money)); }
        }
        /// <summary>
        /// Поле - Дата операции
        /// </summary>
        public DateTime OperationDate
        {
            get { return this.operationdate; }
            set { this.operationdate = value; OnPropertyChanged(nameof(this.operationdate)); }
        }
        /// <summary>
        /// Поле - Номер счета
        /// </summary>
        public long AccountNumber
        {
            get { return this.accountnumber; }
            set { this.accountnumber = value; OnPropertyChanged(nameof(this.accountnumber)); }
        }
        /// <summary>
        /// ИД
        /// </summary>
        private int id;
        /// <summary>
        /// Тип операции
        /// </summary>
        private int operationid;
        /// <summary>
        /// Сумма
        /// </summary>
        private decimal money;
        /// <summary>
        /// Дата операции
        /// </summary>
        private DateTime operationdate;
        /// <summary>
        /// Номер счета
        /// </summary>
        private long accountnumber;

       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
