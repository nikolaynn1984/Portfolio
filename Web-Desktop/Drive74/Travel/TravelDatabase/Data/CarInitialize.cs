using TravelDatabase.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelDatabase.Data
{
    static class CarInitialize
    {
        public static void Initialize(TravelContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cars.Any()) return;

            var cars = new List<Car>()
            {
                new Car(){Id = 1, CarMarkId = 1, CarModelId = 1, Years = 2005, PlaceNumber = "А356РВ700"},
                new Car(){Id = 2, CarMarkId = 1, CarModelId = 3, Years = 2012, PlaceNumber = "Б467РВ700"},
                new Car(){Id = 3, CarMarkId = 2, CarModelId = 5, Years = 2016, PlaceNumber = "В567РВ700"},
                new Car(){Id = 4, CarMarkId = 4, CarModelId = 8, Years = 1996, PlaceNumber = "Г676РВ700"},
                new Car(){Id = 5, CarMarkId = 2, CarModelId = 7, Years = 2008, PlaceNumber = "Д856РВ700"},
                new Car(){Id = 6, CarMarkId = 5, CarModelId = 11, Years = 2017, PlaceNumber = "Е956РВ700"},
                new Car(){Id = 7, CarMarkId = 4, CarModelId = 9, Years = 2010, PlaceNumber = "Р056РВ700"}

            };

            using (var trans = context.Database.BeginTransaction())
            {
                foreach (var item in cars)
                {
                    context.Cars.Add(item);
                }

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Cars] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Cars] OFF");
                trans.Commit();
            }
        }
    }
}
