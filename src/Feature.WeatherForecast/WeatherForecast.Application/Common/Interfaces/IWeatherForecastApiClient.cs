using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using WeatherForecast.Application.Common.Models;
using WeatherForecast.Application.Features.GetWeatherForecasts;

namespace WeatherForecast.Application.Common.Interfaces
{
    public interface IWeatherForecastApiClient
    {
        /// <summary>
        /// Gets the weather forecast for the next X days
        /// </summary>
        /// <param name="days"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<WeatherForecastDetails>> GetAsync(int days, CancellationToken cancellationToken);
    }
}
