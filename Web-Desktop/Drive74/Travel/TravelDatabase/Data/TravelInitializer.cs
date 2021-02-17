using System;
using System.Collections.Generic;
using System.Text;

namespace TravelDatabase.Data
{
    public static class TravelInitializer
    {
        public static void AllInitialize(TravelContext context)
        {
            RegionInitializer.Initialize(context);
            CityInitialize.Initialize(context);
            MarkInitialize.Initialize(context);
            ModelInitialize.Initialize(context);
            CarInitialize.Initialize(context);

        }
    }
}
