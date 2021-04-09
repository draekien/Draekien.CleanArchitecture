using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using MediatR;

using WeatherForecast.Application.Common.Interfaces;
using WeatherForecast.Application.Common.Models;

namespace WeatherForecast.Application.Features.GetWeatherForecasts
{
    public class GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecastDetails>>
    {
        /// <summary>
        ///     The number of days to forecast
        /// </summary>
        [DefaultValue(1)]
        public int Days { get; set; } = 1;

        public class Validator : AbstractValidator<GetWeatherForecastsQuery>
        {
            public Validator()
            {
                RuleFor(x => x.Days)
                    .GreaterThan(0)
                    .LessThanOrEqualTo(5);
            }
        }

        public class Handler : IRequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecastDetails>>
        {
            private readonly IWeatherForecastApiClient _weatherForecastApiClient;

            public Handler(IWeatherForecastApiClient weatherForecastApiClient)
            {
                _weatherForecastApiClient = weatherForecastApiClient;
            }

            /// <inheritdoc />
            public async Task<IEnumerable<WeatherForecastDetails>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<WeatherForecastDetails> result = await _weatherForecastApiClient.GetAsync(request.Days, cancellationToken);

                return result;
            }
        }
    }
}
