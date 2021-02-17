using TravelDatabase.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelDatabase.Data
{
    static class ModelInitialize
    {
        public static void Initialize(TravelContext context)
        {
            context.Database.EnsureCreated();

            if (context.CarModels.Any()) return;

            var models = new List<CarModel>()
            {
                new CarModel(){Id = 1, ModelName = "320", ModelMarkId = 1, ModelResolution = true},
                new CarModel(){Id = 2, ModelName = "M4", ModelMarkId = 1, ModelResolution = true},
                new CarModel(){Id = 3, ModelName = "X5", ModelMarkId = 1, ModelResolution = true},
                new CarModel(){Id = 4, ModelName = "X1", ModelMarkId = 1, ModelResolution = true},
                new CarModel(){Id = 5, ModelName = "A1", ModelMarkId = 2, ModelResolution = true},
                new CarModel(){Id = 6, ModelName = "A3", ModelMarkId = 2, ModelResolution = true},
                new CarModel(){Id = 7, ModelName = "Q3", ModelMarkId = 2, ModelResolution = true},
                new CarModel(){Id = 8, ModelName = "Focus", ModelMarkId = 4, ModelResolution = true},
                new CarModel(){Id = 9, ModelName = "Explorer", ModelMarkId = 4, ModelResolution = true},
                new CarModel(){Id = 10, ModelName = "C-Class", ModelMarkId = 5, ModelResolution = true},
                new CarModel(){Id = 11, ModelName = "E-Class", ModelMarkId = 5, ModelResolution = true}

            };

            using (var trans = context.Database.BeginTransaction())
            {
                foreach (var item in models)
                {
                    context.CarModels.Add(item);
                }

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[CarModel] ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[CarModel] OFF");
                trans.Commit();
            }
        }
    }
}
