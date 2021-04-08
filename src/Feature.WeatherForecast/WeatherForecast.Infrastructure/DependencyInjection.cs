using Microsoft.Extensions.DependencyInjection;

using WeatherForecast.Application.Common.Interfaces;
using WeatherForecast.Infrastructure.Apis;
using WeatherForecast.Infrastructure.Providers;

namespace WeatherForecast.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDateTime, DateTimeProvider>();
            services.AddTransient<IWeatherForecastApiClient, MockWeatherForecastApiClient>();
        }
    }
}
