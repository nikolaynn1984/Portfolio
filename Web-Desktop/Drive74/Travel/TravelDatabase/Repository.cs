using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelDatabase.Interfaces;
using TravelDatabase.Model;

namespace TravelDatabase
{
    public class Repository : IRidesData
    {
        private readonly TravelContext context;
        private int? fromId;
        private int? toId;
        private DateTime? datego;
        public Repository()
        {
            this.context = new TravelContext() ;
        }

        public IEnumerable<Ride> GetRidesList()
        {
            IEnumerable<Ride> item = (IEnumerable<Ride>)context.Rides
                .Include(c => c.FromCityes)
                .Include(c => c.ToCityes)
                .Include(c => c.GetCar)
                .AsNoTracking().ToList();
            return item;
        }
        public IEnumerable<Ride> SearchRides(int? from, int? to, DateTime? date)
        {
            fromId = from;
            toId = to;
            if (date != null) datego = date;
            else datego = DateTime.MinValue;
            List<Ride> item = context.Rides
                .Include(c => c.FromCityes)
                .Include(c => c.ToCityes)
                .Include(c => c.GetCar)
                .AsNoTracking().ToList();

            var newitem = item.FindAll(FindRides);
            return newitem;
        }

        private bool FindRides(Ride obj)
        {
            DateTime testDate = new DateTime(2008, 5, 1, 8, 30, 52);
            if (fromId == 0 && toId == 0 && datego >= testDate) return obj.RideDate == datego;
            else if (fromId != 0 && toId != 0 && datego >= testDate) return obj.FromCityId == fromId & obj.ToCityId == toId & obj.RideDate == datego;
            else if (fromId != 0 && toId != 0 && datego <= testDate) return obj.FromCityId == fromId & obj.ToCityId == toId;
            else if (fromId != 0 && toId == 0 && datego <= testDate) return obj.FromCityId == fromId;
            return false;
        }

        public void AddRide(Ride ride)
        {
            context.Rides.Add(ride);
            context.SaveChanges();
        }
        public IEnumerable<Cityes> GetCityes()
        {
            return context.Cityes;
        }
        public IEnumerable<Car> GetCars()
        {
            return context.Cars;
        }
    }
}
