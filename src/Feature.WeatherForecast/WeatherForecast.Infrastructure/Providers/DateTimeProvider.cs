using System;

using WeatherForecast.Application.Common.Interfaces;

namespace WeatherForecast.Infrastructure.Providers
{
    public class DateTimeProvider : IDateTime
    {
        /// <inheritdoc />
        public DateTime Now => DateTime.Now;

        /// <inheritdoc />
        public DateTime UtcNow => DateTime.UtcNow;

        /// <inheritdoc />
        public DateTime Today => DateTime.Today;
    }
}
