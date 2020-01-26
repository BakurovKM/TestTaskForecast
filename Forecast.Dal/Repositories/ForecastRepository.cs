using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forecast.Dal.Context;
using Forecast.Dal.Context.Models;
using Forecast.Dal.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Forecast.Dal.Repositories
{
    public class ForecastRepository : IForecastRepository
    {
        private readonly string _connectionString;

        public ForecastRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("forecastDb");
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            using (var context = new ForecastContext(_connectionString))
            {
                return await context.Cities.ToArrayAsync();
            }
        }

        public async Task<DayForecast> GetDayForecastAsync(int cityId, DateTime date)
        {
            using (var context = new ForecastContext(_connectionString))
            {
                return await context.DayForecasts
                    .Where(df => df.Date == date.Date && df.CityForecast.City.Id == cityId)
                    .OrderByDescending(df => df.Id)
                    .FirstOrDefaultAsync();
            }
        }
        
        public async Task<City> GetCityAsync(string name)
        {
            using (var context = new ForecastContext(_connectionString))
            {
                return await context.Cities.SingleOrDefaultAsync(city => city.Name.ToLower() == name.ToLower());
            }
        }

        public async Task AddForecastAsync(Context.Models.Forecast forecast)
        {
            using (var context = new ForecastContext(_connectionString))
            {
                context.Forecasts.Add(forecast); 
                await context.SaveChangesAsync();
            }
        }
    }
}