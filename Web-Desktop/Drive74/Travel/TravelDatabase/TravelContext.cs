using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TravelDatabase.Model;

namespace TravelDatabase
{
    public class TravelContext : IdentityDbContext<User>
    {


        //public TravelContext(DbContextOptions<TravelContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectioString = @"Server = (LocalDB)\MSSQLLocalDB;DataBase = TravelDatabaseCore;Trusted_Connection = True; ";
            optionsBuilder.UseSqlServer(connectioString);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarMark> CarMarks { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Cityes> Cityes { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<RideStatus> RideStatuses { get; set; }

    }
}
