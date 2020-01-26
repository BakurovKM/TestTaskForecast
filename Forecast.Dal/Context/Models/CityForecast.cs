using System.Collections.Generic;

namespace Forecast.Dal.Context.Models
{
	public class CityForecast
	{
		public int Id { get; set; }
		
		public Forecast Forecast { get; set; }
		
		public int CityId { get; set; }
		public City City { get; set; }

		public ICollection<DayForecast> DayForecasts { get; set; }
	}
}