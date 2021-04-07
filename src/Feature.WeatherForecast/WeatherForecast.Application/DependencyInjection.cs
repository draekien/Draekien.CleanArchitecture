using System.Reflection;

using FluentValidation;

using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;

using Microsoft.Extensions.DependencyInjection;

namespace WeatherForecast.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
