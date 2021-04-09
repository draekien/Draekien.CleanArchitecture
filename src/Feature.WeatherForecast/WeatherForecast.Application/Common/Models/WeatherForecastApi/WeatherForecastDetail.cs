using System;

namespace WeatherForecast.Application.Common.Models.WeatherForecastApi
{
    public class WeatherForecastDetail
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
    }
}
