using Microsoft.AspNetCore.Diagnostics;

namespace WeatherForecast.Application.Common.ProblemDetailObjects
{
    public static class ExceptionHandlerFeatureExtensions
    {
        public static string GetInstance(this IExceptionHandlerFeature feature)
        {
            return feature switch
            {
                ExceptionHandlerFeature e => e.Path,
                _ => "unknown"
            };
        }
    }
}
