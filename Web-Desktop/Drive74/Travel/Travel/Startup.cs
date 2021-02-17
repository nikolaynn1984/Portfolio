using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelDatabase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TravelDatabase.Model;
using Microsoft.AspNetCore.Identity;
using TravelDatabase.Interfaces;

namespace Travel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //string connection = Configuration.GetConnectionString("TravelConnection");
            services.AddDbContext<TravelContext>();
            services.AddTransient<IRidesData, Repository>();
            
            services.AddMvc();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TravelContext>()    
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequiredLength = 6; //Минимальное количество знаков в пароле

                option.Lockout.MaxFailedAccessAttempts = 10; //Количество попыток входа до блокировки
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20); // Время блокировки аккаунта
                option.Lockout.AllowedForNewUsers = true;
            });

            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.HttpOnly = true;
                option.Cookie.Expiration = TimeSpan.FromMinutes(30);
                option.LoginPath = "/Account/Login";
                option.LogoutPath = "/Account/Logout";
                option.SlidingExpiration = true;
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(t =>
            {
                t.MapRoute(
                    name: "default",
                    template: "{controller=MainView}/{action=Index}/{id?}");
            });
        }
    }
}
