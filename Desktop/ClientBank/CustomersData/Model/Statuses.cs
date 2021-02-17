using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomersData.Model
{
    public class Statuses : INotifyPropertyChanged
    {
        /// <summary>
        /// Конструктор - Статусы   
        /// </summary>
        /// <param name="Id">Ид</param>
        /// <param name="DepositRate">Ставка по вкладу</param>
        /// <param name="Status">Статус</param>
        /// <param name="CreditRate">Ставка по кредиту</param>
        public Statuses(int Id, int DepositRate, string Status, int CreditRate)
        {

            this.Id = Id;
            this.Status = Status;
            this.DepositRate = DepositRate;
            this.CreditRate = CreditRate;
        }

        /// <summary>
        /// ИД
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(nameof(this.id)); }

        }
        /// <summary>
        /// Статус
        /// </summary>
        public string Status
        {
            get { return this.status; }
            set { this.status = value; OnPropertyChanged(nameof(this.status)); }
        }
        /// <summary>
        /// Ставка по вкладу
        /// </summary>
        public int DepositRate
        {
            get { return this.depositrate; }
            set { this.depositrate = value; OnPropertyChanged(nameof(this.depositrate)); }
        }
        /// <summary>
        /// Поле - Ставка по кредиту
        /// </summary>
        public int CreditRate
        {
            get { return this.creditrate; }
            set { this.creditrate = value; OnPropertyChanged(nameof(this.creditrate)); }
        }
        /// <summary>
        /// Поле - ИД
        /// </summary>
        private int id;
        /// <summary>
        /// Поле - Статус
        /// </summary>
        private string status;
        /// <summary>
        /// Ставка по вкладу
        /// </summary>
        private int depositrate;
        /// <summary>
        /// Ставка по кредиту
        /// </summary>
        private int creditrate;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
