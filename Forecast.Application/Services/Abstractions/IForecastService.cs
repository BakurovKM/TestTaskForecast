using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Dal.Context.Models;

namespace Forecast.Application.Services.Abstractions
{
    public interface IForecastService
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<DayForecast> GetDayForecastAsync(int cityId, DateTime date);
    }
}