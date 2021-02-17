using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfTravelApp.Model
{
    /// <summary>
    /// Поездки
    /// </summary>
    public class Ride : INotifyPropertyChanged
    {
        public int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(nameof(this.Id)); }
        }
        public int FromCityId
        {
            get { return this.fromCityId; }
            set { this.fromCityId = value; OnPropertyChanged(nameof(this.FromCityId)); }
        }
        public int ToCityId
        {
            get { return this.toCityId; }
            set { this.toCityId = value; OnPropertyChanged(nameof(this.ToCityId)); }
        }
        public DateTime RideDate
        {
            get { return this.rideDate; }
            set { this.rideDate = value; OnPropertyChanged(nameof(this.RideDate)); }
        }
        public int FreePlace
        {
            get { return this.freePlace; }
            set { this.freePlace = value; OnPropertyChanged(nameof(this.FreePlace)); }
        }
        public int CarId
        {
            get { return this.carId; }
            set { this.carId = value; OnPropertyChanged(nameof(this.CarId)); }
        }
        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; OnPropertyChanged(nameof(this.Price)); }
        }

        public string FromCity
        {
            get { return Repository.GetCityId(this.FromCityId); }
        }
        public string ToCity
        {
            get { return Repository.GetCityId(this.ToCityId); }
        }
        public string CarPlace
        {
            get { return Repository.GetCarId(this.CarId); }
        }

        /// <summary>
        /// Ид
        /// </summary>
        private int id;
        /// <summary>
        /// От куда - ИД город
        /// </summary>
        private int fromCityId;
        /// <summary>
        /// Куда - ИД город
        /// </summary>
        private int toCityId;
        /// <summary>
        /// Дата поездки
        /// </summary>
        private DateTime rideDate;
        /// <summary>
        /// Цена поездки
        /// </summary>
        private int freePlace;
        /// <summary>
        /// Ид Машины
        /// </summary>
        private int carId;
        /// <summary>
        /// Цена поездки
        /// </summary>
        private decimal price;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
