using System;
using System.Collections.Generic;

namespace Forecast.Dal.Context.Models
{
	public class Forecast
	{
		public int Id { get; set; }
		
		public DateTime Date { get; set; }
		
		public ICollection<CityForecast> CityForecasts { get; set; }
	}
}
