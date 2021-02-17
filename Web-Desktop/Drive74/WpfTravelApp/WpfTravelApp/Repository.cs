using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfTravelApp.Model;

namespace WpfTravelApp
{
    public class Repository
    {
        private readonly string localHost;
        private HttpClient HttpClient;
        public static List<Cityes> Cityes;
        public static List<Car> GetCars;
        public static ObservableCollection<Ride> RidesList;
        public Repository(string local)
        {
            this.localHost = local;
            HttpClient = new HttpClient();
            RidesList = new ObservableCollection<Ride>();
            Cityes = new List<Cityes>();
            GetCars = new List<Car>();
            ReadCities();
            ReadCar();
            ReadData();
        }

        private void ReadCities()
        {
            string url = $"{localHost}api/values/cities";
            string city = HttpClient.GetStringAsync(url).Result;
            Cityes = JsonConvert.DeserializeObject<List<Cityes>>(city);
        }
        private void ReadCar()
        {
            string url = $"{localHost}api/values/cars";
            string cars = HttpClient.GetStringAsync(url).Result;
            GetCars = JsonConvert.DeserializeObject<List<Car>>(cars);
        }

        internal bool ValidItems(int from, int to, int car, string dates, string free, string price, out List<string> msgerror)
        {
            bool result = true;
            DateTime date;
            int newFree;
            decimal newPrice;
            msgerror = new List<string>();
            if(from == 0) { result = false; msgerror.Add("Не выбран город - Откуда"); }
            if (to == 0) { result = false; msgerror.Add("Не выбран город - Куда"); }
            if (car == 0) { result = false; msgerror.Add("Не выбран автомобиль"); }
            if(from != 0) { if (from == to) { result = false; msgerror.Add("Поля - Откуда и Куда не должны иметь одинаковое значение"); } }
            if(!DateTime.TryParse(dates, out date)){ result = false; msgerror.Add("Не верный формат - Дата поездки"); }

            if(!Int32.TryParse(free, out newFree)) { result = false; msgerror.Add("Поле - Количество мест должно содержать только цифры"); }
            else { if(newFree == 0) { result = false; msgerror.Add("Количество мест не должно быть меньше 1"); } }

            if (!Decimal.TryParse(price, out newPrice)) { result = false; msgerror.Add("Поле Цена должна содержать только цифры"); }
            else { if (newPrice < 100) { result = false; msgerror.Add("Цена поездки не должны быть меньше 100р"); } }

            if (result) AddItems(from, to, car, date, newFree, newPrice);

            return result;
        }

        private void AddItems(int from, int to, int car, DateTime date, int newFree, decimal newPrice)
        {
            var item = new Ride()
            {
                FromCityId = from,
                ToCityId = to,
                CarId = car,
                RideDate = date,
                FreePlace = newFree,
                Price = newPrice
            };

            string url = $"{localHost}api/values";
            var add = HttpClient.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
            ReadData();
        }

        public void ReadData()
        {
            string url = $"{localHost}api/values/rides";

            string r = HttpClient.GetStringAsync(url).Result;

            var items = JsonConvert.DeserializeObject<IEnumerable<Ride>>(r);
            RidesList = new ObservableCollection<Ride>(items);
        }

        

        private bool ValidItems(Ride item)
        {
            bool resul = true;
            DateTime date = DateTime.Now;

            if (item.FromCityId == 0) resul = false;
            if (item.ToCityId == 0) resul = false;
            if (item.CarId == 0) resul = false;
            if (item.RideDate < date) resul = false;

            return resul;
        }

        public static string GetCityId(int id)
        {
            try
            {
                string item = Cityes.Single(s => s.Id == id).City;
                return item;
            }
            catch
            {
                return "Нет данных";
            }
        }
        public static string GetCarId(int id)
        {
            try
            {
                string item = GetCars.Single(s => s.Id == id).PlaceNumber;
                return item;
            }
            catch
            {
                return "Нет данных";
            }
        }

        
    }
}
