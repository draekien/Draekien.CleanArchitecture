using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using MediatR;

namespace WeatherForecast.Application.Features.GetWeatherForecasts
{
    public class GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecastDetails>>
    {
        /// <summary>
        ///     The number of days to forecast
        /// </summary>
        public int Days { get; set; }

        public class Validator : AbstractValidator<GetWeatherForecastsQuery>
        {
            public Validator()
            {
                RuleFor(x => x.Days).NotNull().GreaterThan(0).LessThan(5);
            }
        }

        public class Handler : IRequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecastDetails>>
        {
            private static readonly string[] Summaries = {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            /// <inheritdoc />
            public async Task<IEnumerable<WeatherForecastDetails>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
            {
                var rng = new Random();
                IEnumerable<WeatherForecastDetails> result = Enumerable.Range(1, request.Days)
                                                                .Select(index => new WeatherForecastDetails
                                                                {
                                                                    Date = DateTime.Now.AddDays(index),
                                                                    TemperatureC = rng.Next(-20, 55),
                                                                    Summary = Summaries[rng.Next(Summaries.Length)]
                                                                });

                return await Task.FromResult(result);
            }
        }
    }
}
