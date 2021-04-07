using Hellang.Middleware.ProblemDetails;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WeatherForecast.Api.OnStart
{
    public static class ConfigureProblemDetails
    {
        public static void Defaults(this ProblemDetailsOptions options)
        {
            options.IncludeExceptionDetails = (context, _) =>
            {
                var environment = context.RequestServices.GetRequiredService<IHostEnvironment>();
                return environment.IsDevelopment();
            };
        }
    }
}
