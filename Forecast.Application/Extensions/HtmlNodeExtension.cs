using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Forecast.Application.Extensions
{
	public static class HtmlNodeExtension
	{
		private static readonly Regex RegExp = new Regex("[0-9,]+");

		public static HtmlNodeCollection GetCities(this HtmlNode currentNode)
		{
			return currentNode.SelectNodes(@"//noscript[@id='noscript']/a[starts-with(@data-url, '/weather-')]");
		}

		public static HtmlNode GetForecast(this HtmlNode currentNode)
		{
			return currentNode.SelectSingleNode(@"//div[@data-widget-id='forecast']");
		}

		public static HtmlNode[] GetDescribes(this HtmlNode currentNode)
		{
			return currentNode.SelectNodes("//div[@class='widget__row widget__row_table widget__row_icon']/div").ToArray();
		}

		public static HtmlNode[] GetTemperatures(this HtmlNode currentNode)
		{
			return currentNode.SelectNodes("//div[@class='templine w_temperature']/div/div/div").ToArray();
		}

		public static HtmlNode[] GetWinds(this HtmlNode currentNode)
		{
			return currentNode.SelectNodes("//div[@class='widget__row widget__row_table widget__row_wind-or-gust']/div").ToArray();
		}

		public static HtmlNode[] GetPrecipitations(this HtmlNode currentNode)
		{
			return currentNode.SelectNodes("//div[@class='widget__row widget__row_table widget__row_precipitation']/div").ToArray();
		}

		public static string GetDescribe(this HtmlNode currentNode)
		{
			return currentNode.SelectSingleNode(".//div/span").Attributes["data-text"]?.Value;
		}

		public static string GetTemperatureMax(this HtmlNode currentNode)
		{
			return currentNode.SelectSingleNode(".//div[@class='maxt']/span[@class='unit unit_temperature_c']")?.InnerHtml.Replace("&minus;", "-");
		}

		public static string GetTemperatureMin(this HtmlNode currentNode)
		{
			return currentNode.SelectSingleNode(".//div[@class='mint']/span[@class='unit unit_temperature_c']")?.InnerHtml.Replace("&minus;", "-");
		}

		public static string GetWindSpeed(this HtmlNode currentNode)
		{
			return RegExp.Match(currentNode.SelectSingleNode(".//span[@class='unit unit_wind_m_s']").InnerText).Value;
		}

		public static string GetPrecipitationLevel(this HtmlNode currentNode)
		{
			return RegExp.Match(currentNode.SelectSingleNode(".//div/div").InnerText).Value;
		}

		public static string GetCityName(this HtmlNode currentNode)
		{
			return currentNode.Attributes["data-name"].Value;
		}

		public static string GetCityUrl(this HtmlNode currentNode)
		{
			return currentNode.Attributes["data-url"].Value;
		}
	}
}
