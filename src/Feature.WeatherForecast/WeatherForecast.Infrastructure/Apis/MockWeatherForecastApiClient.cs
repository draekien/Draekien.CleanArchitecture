using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WeatherForecast.Application.Common.Interfaces;
using WeatherForecast.Application.Common.Models;

namespace WeatherForecast.Infrastructure.Apis
{
    public class MockWeatherForecastApiClient : IWeatherForecastApiClient
    {
        private readonly IDateTime _dateTime;
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public MockWeatherForecastApiClient(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<WeatherForecastDetails>> GetAsync(int days, CancellationToken cancellationToken)
        {
            var rng = new Random();
            IEnumerable<WeatherForecastDetails> result = Enumerable.Range(1, days)
                                                                   .Select(index => new WeatherForecastDetails
                                                                   {
                                                                       Date = _dateTime.Now.AddDays(index),
                                                                       TemperatureC = rng.Next(-20, 55),
                                                                       Summary = Summaries[rng.Next(Summaries.Length)]
                                                                   });

            return await Task.FromResult(result);
        }
    }
}
