using TravelDatabase.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelDatabase.Data
{
    static class CityInitialize 
    {
        public static void Initialize(TravelContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cityes.Any()) return;

            var city = new List<Cityes>()
            {
                new Cityes(){Id = 1, City = "Москва", RegionId = 1, Acting = true},
                new Cityes(){Id = 2, City = "Белгород", RegionId = 2, Acting = true},
                new Cityes(){Id = 3, City = "Воронеж", RegionId = 3, Acting = true},
                new Cityes(){Id = 4, City = "Калуга", RegionId = 4, Acting = true},
                new Cityes(){Id = 5, City = "Санкт-петербург", RegionId = 5, Acting = true},
                new Cityes(){Id = 6, City = "Челябинск", RegionId = 6, Acting = true}
            };
            using (var trans = context.Database.BeginTransaction())
            {
                foreach (var item in city)
                {
                    context.Cityes.Add(item);
                }

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Cityes] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Cityes] OFF");
                trans.Commit();
            }
        }
    }
}
