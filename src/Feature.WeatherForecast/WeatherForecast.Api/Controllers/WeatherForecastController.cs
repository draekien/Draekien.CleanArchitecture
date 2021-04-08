using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WeatherForecast.Application.Common.Models;
using WeatherForecast.Application.Features.GetWeatherForecasts;

namespace WeatherForecast.Api.Controllers
{
    public class WeatherForecastController : ApiControllerBase
    {
        /// <summary>
        ///     Gets the Weather Forecast for a specified number of days
        /// </summary>
        /// <param name="query">Number of days to forecast</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A list of <see cref="WeatherForecastDetails"/></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecastDetails>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetWeatherForecastsQuery query, CancellationToken cancellationToken = default)
        {
            IEnumerable<WeatherForecastDetails> result = await Mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
