using System;

namespace WeatherForecast.Application.Features.GetWeatherForecasts
{
    /// <summary>
    /// The Details of a Weather Forecast
    /// </summary>
    public class WeatherForecastDetails
    {
        /// <summary>
        /// The DateTime of the specific forecast
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The temperate in Celsius for the specified Date
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// The temperature in Fahrenheit for the specified Date
        /// </summary>
        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);

        /// <summary>
        /// A summary of the forecast conditions for the specified Date
        /// </summary>
        public string Summary { get; set; }
    }
}
