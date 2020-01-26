using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Application.Services.Abstractions;
using Forecast.Dal.Context.Models;
using Forecast.Dal.Repositories.Abstractions;

namespace Forecast.Application.Services
{
    public class ForecastService : IForecastService
    {
        private readonly IForecastRepository _forecastRepository;

        public ForecastService(IForecastRepository forecastRepository)
        {
            _forecastRepository = forecastRepository;
        }
        
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _forecastRepository.GetCitiesAsync();
        }

        public async Task<DayForecast> GetDayForecastAsync(int cityId, DateTime date)
        {
            return await _forecastRepository.GetDayForecastAsync(cityId, date);
        }
    }
}