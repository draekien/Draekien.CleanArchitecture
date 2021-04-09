using System.Linq;
using System.Net.Mime;
using System.Reflection;

using FluentValidation.AspNetCore;

using Hellang.Middleware.ProblemDetails.Mvc;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json.Converters;

using WeatherForecast.Application.Common.ProblemDetailObjects;

namespace WeatherForecast.Api.OnStart
{
    public static class ConfigureControllers
    {
        public static void AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers(SetControllerConfiguration)
                    .AddProblemDetailsConventions()
                    .AddFluentValidation(SetFluentValidationConfiguration)
                    .AddNewtonsoftJson(SetNewtonsoftJsonConfiguration);

            services.AddControllers(RemoveExcessNewtonsoftJsonFormatters);
        }

        private static void RemoveExcessNewtonsoftJsonFormatters(MvcOptions options)
        {
            options.InputFormatters.RemoveType<NewtonsoftJsonPatchInputFormatter>();
            var jsonInputFormatter = (NewtonsoftJsonInputFormatter) options.InputFormatters.Single(formatter => formatter is NewtonsoftJsonInputFormatter);
            jsonInputFormatter.SupportedMediaTypes.Clear();
            jsonInputFormatter.SupportedMediaTypes.Add("application/json");
        }

        private static void SetNewtonsoftJsonConfiguration(MvcNewtonsoftJsonOptions options)
        {
            options.UseCamelCasing(true);
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        }

        private static void SetFluentValidationConfiguration(FluentValidationMvcConfiguration options)
        {
            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            options.AutomaticValidationEnabled = false; // so we can use our own exception handler
        }

        private static void SetControllerConfiguration(MvcOptions options)
        {
            options.ReturnHttpNotAcceptable = true;
            options.RespectBrowserAcceptHeader = true;

            options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
            options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));

            options.Filters.Add(new ProducesResponseTypeAttribute(typeof(BadRequestProblemDetails), StatusCodes.Status400BadRequest));
            options.Filters.Add(new ProducesResponseTypeAttribute(typeof(UnhandledExceptionProblemDetails), StatusCodes.Status500InternalServerError));

            options.Conventions.Add(new ControllerNameFromGroupConvention());
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
