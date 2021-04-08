using System;

namespace WeatherForecast.Application.Common.Interfaces
{
    public interface IDateTime
    {
        /// <inheritdoc cref="DateTime.Now"/>
        DateTime Now { get; }

        /// <inheritdoc cref="DateTime.UtcNow"/>
        DateTime UtcNow { get; }

        /// <inheritdoc cref="DateTime.Today"/>
        DateTime Today { get; }
    }
}
