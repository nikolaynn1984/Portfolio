using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountData.Model
{
    public class Repayment : INotifyPropertyChanged
    {
        /// <summary>
        /// Конструктор - погашения кредитов
        /// </summary>
        /// <param name="Id">ИД</param>
        /// <param name="CreditId">ИД Кредита</param>
        /// <param name="RepaymentDate">Дата погашения</param>
        /// <param name="PlanningDate">Дата планируемого платежа</param>
        /// <param name="Performed">Проведен / не проведен</param>
        /// <param name="Amount">Сумма</param>
        public Repayment(int Id, int CreditId, DateTime RepaymentDate, DateTime PlanningDate, bool Performed, decimal Amount)
        {
            this.Id = Id;
            this.CreditId = CreditId;
            this.RepaymentDate = RepaymentDate;
            this.Amount = Amount;
            this.PlanningDate = PlanningDate;
            this.Performed = Performed;
        }

        public Repayment()
        {
        }

        /// <summary>
        /// ИД
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(nameof(this.Id)); }
        }
        /// <summary>
        /// ИД Кредита
        /// </summary>
        public int CreditId
        {
            get { return this.creditid; }
            set { this.creditid = value; OnPropertyChanged(nameof(this.CreditId)); }
        }
        /// <summary>
        /// Дата погашения
        /// </summary>
        public DateTime RepaymentDate
        {
            get { return this.repaymentdate; }
            set { this.repaymentdate = value; OnPropertyChanged(nameof(this.RepaymentDate)); }
        }
        /// <summary>
        /// Дата плановуемого платежа
        /// </summary>
        public DateTime PlanningDate
        {
            get { return this.planningdate; }
            set { this.planningdate = value; OnPropertyChanged(nameof(this.PlanningDate)); }
        }
        /// <summary>
        /// Сумма погашения
        /// </summary>
        public decimal Amount
        {
            get { return this.amount; }
            set { this.amount = value; OnPropertyChanged(nameof(this.Amount)); }
        }
        /// <summary>
        /// Проведен / не проведен
        /// </summary>
        public bool Performed
        {
            get { return this.performed; }
            set { this.performed = value; OnPropertyChanged(nameof(this.Performed)); }
        }
        /// <summary>
        /// ИД
        /// </summary>
        private int id;
        /// <summary>
        /// ИД кредита
        /// </summary>
        private int creditid;
        /// <summary>
        /// Дата погашения
        /// </summary>
        private DateTime repaymentdate;
        /// <summary>
        /// Сумма
        /// </summary>
        private decimal amount;
        /// <summary>
        /// Выполнено/Не выполнено
        /// </summary>
        private bool performed;
        /// <summary>
        /// Дата планированного платежа
        /// </summary>
        private DateTime planningdate;

       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
