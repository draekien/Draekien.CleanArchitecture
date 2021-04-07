using System.Net.Mime;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

using WeatherForecast.Application.Common.ProblemDetailObjects;

namespace WeatherForecast.Api.OnStart
{
    public static class ConfigureControllers
    {
        public static void AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                options.RespectBrowserAcceptHeader = true;

                options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
                options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));

                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(BadRequestProblemDetails), StatusCodes.Status400BadRequest));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(UnhandledExceptionProblemDetails), StatusCodes.Status500InternalServerError));

                options.Conventions.Add(new ControllerNameFromGroupConvention());
            }).AddFluentValidation();
        }
    }

    public class ControllerNameFromGroupConvention : IControllerModelConvention
    {
        /// <inheritdoc />
        public void Apply(ControllerModel controller)
        {
            if (string.IsNullOrWhiteSpace(controller.ApiExplorer.GroupName)) return;

            controller.ControllerName = controller.ApiExplorer.GroupName;
        }
    }
}
