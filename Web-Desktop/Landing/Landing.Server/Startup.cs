using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Landing.Server.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Landing.Server.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Landing.Server.Hubs;
using Landing.Server.Data;
using Landing.Model.Logging;

namespace Landing.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<ServerLogers>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.TokenOption();
            
            services.LandingTransient();

            services.AddSignalR();
            services.AddHostedService<Connection>();
            services.AddHostedService<MessagesBotService>();
            


            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Landing", Version = "v1" });
                c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Description = "Заголовок авторизации JWT с использованием схемы носителя"
                });
                c.OperationFilter<AuthResponses.AuthResponsesOperationFilter>();
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Log.Info("Запуск приложения Landing", "Startup");

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LandingHub>("/landhub");
                endpoints.MapControllers();
            });
        }
    }
}
