using TravelDatabase.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelDatabase.Data
{
    static class MarkInitialize
    {
        public static void Initialize(TravelContext context)
        {
            context.Database.EnsureCreated();

            if (context.CarMarks.Any()) return;

            var mark = new List<CarMark>()
            {
                new CarMark(){Id = 1, MarkName = "BMW", MarkResolution = true},
                new CarMark(){Id = 2, MarkName = "Audi", MarkResolution = true},
                new CarMark(){Id = 3, MarkName = "Ford", MarkResolution = true},
                new CarMark(){Id = 4, MarkName = "Mercedes-Benz", MarkResolution = true}

            };

            using (var trans = context.Database.BeginTransaction())
            {
                foreach (var item in mark)
                {
                    context.CarMarks.Add(item);
                }

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[CarMark] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[CarMark] OFF");
                trans.Commit();
            }
        }
    }
}
