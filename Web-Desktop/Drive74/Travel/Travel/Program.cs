using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TravelDatabase;
using TravelDatabase.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Travel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var init = CreateWebHostBuilder(args);

            using(var scope = init.Services.CreateScope())
            {
                    var s = scope.ServiceProvider;
                    var c = s.GetRequiredService<TravelContext>();
                    TravelInitializer.AllInitialize(c);
            }
            init.Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
