using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Dal.Context.Models;

namespace Forecast.Dal.Repositories.Abstractions
{
    public interface IForecastRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<DayForecast> GetDayForecastAsync(int cityId, DateTime date);
        
        Task<City> GetCityAsync(string name);

        Task AddForecastAsync(Context.Models.Forecast forecast);
    }
}