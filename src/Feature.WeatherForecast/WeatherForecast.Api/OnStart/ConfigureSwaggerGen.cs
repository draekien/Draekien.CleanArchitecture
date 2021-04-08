using System;
using System.IO;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

using WeatherForecast.Application.Common.Filters;

namespace WeatherForecast.Api.OnStart
{
    public static class ConfigureSwaggerGen
    {
        public static void AddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WeatherForecast.Api",
                    Version = "v1",
                });

                var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);

                var applicationXmlFile = $"{typeof(Application.DependencyInjection).Assembly.GetName().Name}.xml";
                string applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFile);

                c.EnableAnnotations();
                c.IncludeXmlComments(apiXmlPath, includeControllerXmlComments: true);
                c.IncludeXmlComments(applicationXmlPath, includeControllerXmlComments: true);

                c.DocInclusionPredicate((_,_) => true);
                c.AddFluentValidationRules();

                c.ExampleFilters();
                c.OperationFilter<AddCorrelationIdHeaderParameter>();
            });

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
