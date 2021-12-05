using Landing.Library.Interfaces;
using Landing.Library.Interfaces.Server;
using Landing.Server.Auth;
using Landing.Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Landing.Server.Options
{
    public static class ServiceCollection
    {
        public static void TokenOption(this IServiceCollection services)
        {
            var tokenValidateParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidateParams);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwt =>
                {
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = tokenValidateParams;
                });
        }

        public static void LandingTransient(this IServiceCollection services)
        {
            services.AddTransient<IApplicationStatus, StatusData>();
            services.AddTransient<IApplication, ApplicationData>();
            services.AddTransient<IServices, ServiceData>();
            services.AddTransient<IBlog, BlogData>();
            services.AddTransient<ISocial, SocialData>();
            services.AddTransient<IPortfolio, PortfolioData>();
            services.AddTransient<IMainMenu, MenuData>();
            services.AddTransient<IImagesFile, ImagesData>();
            services.AddTransient<IAccountHandler, AuthHandler>();
            services.AddTransient<IPageView, PageData>();
            services.AddSingleton<IMessagesBot, MessagesBotData>();
            services.AddSingleton<IBotMessage, MessagesBotService>();
        }

        public static void AuthServiceOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "nikolay";
                
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "//Login";
                options.AccessDeniedPath = "/AccessDenied";
                options.SlidingExpiration = true;
            });
        }
    }
}
