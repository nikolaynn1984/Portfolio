using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomersData.Model
{
    public class Cities : INotifyPropertyChanged
    {
        public Cities() { }
        /// <summary>
        /// Конструктор - города
        /// </summary>
        /// <param name="Id">ИД</param>
        /// <param name="City">Город</param>
        public Cities(int Id, string City)
        {
            this.Id = Id;
            this.City = City;
        }

        /// <summary>
        /// Ид
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(nameof(this.id)); }
        }
        /// <summary>
        /// Город
        /// </summary>
        public string City
        {
            get { return this.city; }
            set { this.city = value; OnPropertyChanged(nameof(this.city)); }
        }

        /// <summary>
        /// Поле - ИД
        /// </summary>
        private int id;
        /// <summary>
        /// Поле - Город
        /// </summary>
        private string city;



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
