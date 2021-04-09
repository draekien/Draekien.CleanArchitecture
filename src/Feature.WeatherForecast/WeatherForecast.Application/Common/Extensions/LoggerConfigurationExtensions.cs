using System;

using Serilog;
using Serilog.Configuration;

using WeatherForecast.Application.Common.Enrichers;

namespace WeatherForecast.Application.Common.Extensions
{
    public static class LoggerConfigurationExtensions
    {
        public static LoggerConfiguration WithCorrelationIdHeader(this LoggerEnrichmentConfiguration configuration, string headerKey = "x-correlation-id")
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            return configuration.With(new CorrelationIdHeader(headerKey));
        }
    }
}
