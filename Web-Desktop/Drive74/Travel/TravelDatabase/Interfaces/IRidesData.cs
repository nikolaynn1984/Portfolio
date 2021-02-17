using System;
using System.Collections.Generic;
using System.Text;
using TravelDatabase.Model;

namespace TravelDatabase.Interfaces
{
    public interface IRidesData
    {
        IEnumerable<Ride> GetRidesList();
        IEnumerable<Ride> SearchRides(int? from, int? to, DateTime? date);
        void AddRide(Ride ride);
        IEnumerable<Cityes> GetCityes();
        IEnumerable<Car> GetCars();
    }
}
