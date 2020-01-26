using Forecast.Application.Services;
using Forecast.Application.Services.Abstractions;
using Forecast.Application.Settings;
using Forecast.Dal.Context;
using Forecast.Dal.Repositories;
using Forecast.Dal.Repositories.Abstractions;
using Forecast.Host.BackgroundServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Forecast.Host
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.Configure<GismeteoParserSettings>(_configuration.GetSection("GismeteoParserSettings"));

            services.AddScoped<IForecastService, ForecastService>();
            services.AddScoped<IGismeteoParser, GismeteoParser>();
            
            services.AddScoped<IForecastRepository, ForecastRepository>();

            services.AddHostedService<ForecastUpdaterBackgroundService>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Forecast}/{action=Index}/");
            });
            
            var dbUpdater = new ForecastContextMigrations(_configuration.GetConnectionString("forecastDb"));
            dbUpdater.ApplyMigrations();
        }
    }
}