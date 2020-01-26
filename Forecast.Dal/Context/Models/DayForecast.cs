using System;

namespace Forecast.Dal.Context.Models
{
	public class DayForecast
	{
		public int Id { get; set; }
		
		public DateTime Date { get; set; }

		public CityForecast CityForecast { get; set; }
		
		public string Describe { get; set; }
		
		public int? TemperatureMax { get; set; }
		
		public int? TemperatureMin { get; set; }
		
		public decimal WindSpeedMs { get; set; }
		
		public decimal Precipitation { get; set; }
	}
}
