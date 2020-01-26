using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Forecast.Application.Extensions;
using Forecast.Application.Services.Abstractions;
using Forecast.Application.Settings;
using Forecast.Dal.Context.Models;
using Forecast.Dal.Repositories.Abstractions;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;

namespace Forecast.Application.Services
{
    public class GismeteoParser : IGismeteoParser
    {
        private readonly IForecastRepository _forecastRepository;
        private readonly GismeteoParserSettings _parserSettings;

        public GismeteoParser(IOptions<GismeteoParserSettings> gismeteoParserSettings,
            IForecastRepository forecastRepository)
        {
            _forecastRepository = forecastRepository;
            _parserSettings = gismeteoParserSettings.Value;
        }

        public async Task ParseAsync()
        {
            var forecast = new Dal.Context.Models.Forecast()
            {
                Date = DateTime.Now,
                CityForecasts = new List<CityForecast>()
            };

            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(_parserSettings.Resource);

            foreach (var cityData in doc.DocumentNode.GetCities())
            {
                var cityName = cityData.GetCityName();
                var city = await _forecastRepository.GetCityAsync(cityName) ?? new City {Name = cityName};

                var cityDoc =
                    await web.LoadFromWebAsync(
                        $"{_parserSettings.Resource}{cityData.GetCityUrl()}{_parserSettings.Period}");
                var forecastData = cityDoc.DocumentNode.GetForecast();

                var describes = forecastData.GetDescribes();
                var temperatures = forecastData.GetTemperatures();
                var winds = forecastData.GetWinds();
                var precipitations = forecastData.GetPrecipitations();

                var cityForecast = GetCityForecast(forecast, city);

                for (var i = 0; i < temperatures.Length; i++)
                {
                    cityForecast.DayForecasts.Add(GetDayForecast(i, describes, winds, precipitations, temperatures));
                }

                forecast.CityForecasts.Add(cityForecast);
            }

            await _forecastRepository.AddForecastAsync(forecast);
        }

        private static CityForecast GetCityForecast(Dal.Context.Models.Forecast forecast, City city)
        {
            var cityForecast = new CityForecast { DayForecasts = new List<DayForecast>(), Forecast = forecast };
            if (city.Id != 0)
            {
                cityForecast.CityId = city.Id;
            }
            else
            {
                cityForecast.City = city;
            }
            return cityForecast;
        }

        private static DayForecast GetDayForecast(int position, HtmlNode[] describes, HtmlNode[] winds,
            HtmlNode[] precipitations, HtmlNode[] temperatures)
        {
            int? NullableIntParse(string data)
            {
                return int.TryParse(data, out var mint)
                    ? (int?) mint
                    : null;
            }

            decimal DecimalParse(string data)
            {
                return decimal.Parse(data.Replace(",", "."), CultureInfo.InvariantCulture);
            }

            var dayForecast = new DayForecast
            {
                Describe = describes[position].GetDescribe(),
                Date = DateTime.Now.AddDays(position),
                WindSpeedMs = DecimalParse(winds[position].GetWindSpeed()),
                Precipitation = DecimalParse(precipitations[position].GetPrecipitationLevel()),
                TemperatureMax = NullableIntParse(temperatures[position].GetTemperatureMax()),
                TemperatureMin = NullableIntParse(temperatures[position].GetTemperatureMin())
            };
            return dayForecast;
        }
    }
}