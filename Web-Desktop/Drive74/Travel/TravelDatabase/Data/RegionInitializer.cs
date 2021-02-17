using TravelDatabase.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelDatabase.Data
{
    static class RegionInitializer
    {
        public static void Initialize(TravelContext context)
        {
            context.Database.EnsureCreated();

            if (context.Regions.Any()) return;

            var region = new List<Regions>()
            {
                new Regions(){Id = 1, Region = "Московская область", RegionNumber = 50, RegionActing = true},
                new Regions(){Id = 2, Region = "Белгородская область", RegionNumber = 31, RegionActing = true},
                new Regions(){Id = 3, Region = "Воронежская область", RegionNumber = 36, RegionActing = true},
                new Regions(){Id = 4, Region = "Калужская область", RegionNumber = 40, RegionActing = true},
                new Regions(){Id = 5, Region = "Ленинградская область", RegionNumber = 47, RegionActing = true},
                new Regions(){Id = 6, Region = "Челябинская область", RegionNumber = 74, RegionActing = true}

            };

            using(var trans = context.Database.BeginTransaction())
            {
                foreach(var item in region)
                {
                    context.Regions.Add(item);
                }

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Regions] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Regions] OFF");
                trans.Commit();
            }
        }
    }
}
