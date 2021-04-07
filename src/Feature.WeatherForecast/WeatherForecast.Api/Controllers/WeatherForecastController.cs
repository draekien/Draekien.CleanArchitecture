using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WeatherForecast.Application.Features.GetWeatherForecasts;

namespace WeatherForecast.Api.Controllers
{
    public class WeatherForecastController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecastDetails>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int days = 1, CancellationToken cancellationToken = default)
        {
            var query = new GetWeatherForecastsQuery { Days = days };
            IEnumerable<WeatherForecastDetails> result = await Mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
