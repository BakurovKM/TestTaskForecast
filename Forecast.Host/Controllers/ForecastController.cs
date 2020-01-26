using System;
using System.Linq;
using System.Threading.Tasks;
using Forecast.Application.Services.Abstractions;
using Forecast.Dal.Context.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forecast.Host.Controllers
{
    public class ForecastController : Controller
    {
        private readonly IForecastService _forecastService;

        public ForecastController(IForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        public async Task<IActionResult> Index()
        {
            var cities = await _forecastService.GetCitiesAsync();
            return View(cities.ToDictionary(key => key.Id, value => value.Name));
        }

        [HttpGet]
        [Route("Forecast")]
        public async Task<IActionResult> DayForecast(int cityId, DateTime date)
        {
            var forecast = await _forecastService.GetDayForecastAsync(cityId, date);
            return forecast != null
                ? (IActionResult) Ok(forecast)
                : NotFound();
        }
    }
}