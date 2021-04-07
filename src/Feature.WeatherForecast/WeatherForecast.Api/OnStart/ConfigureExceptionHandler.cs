using System;
using System.Text.Json;

using FluentValidation;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using WeatherForecast.Application.Common.ProblemDetailObjects;

namespace WeatherForecast.Api.OnStart
{
    public static class ConfigureExceptionHandler
    {
        public static void Defaults(this IApplicationBuilder errorApp)
        {
            errorApp.Run(async context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                Exception exception = errorFeature.Error;

                ProblemDetails problemDetails = new UnhandledExceptionProblemDetails(exception, errorFeature, context);

                switch (exception)
                {
                    case InvalidOperationException invalidOperationException:
                        problemDetails = new BadRequestProblemDetails(invalidOperationException, errorFeature, context);
                        break;
                    case ArgumentOutOfRangeException argumentOutOfRangeException:
                        problemDetails = new BadRequestProblemDetails(argumentOutOfRangeException, errorFeature, context);
                        break;
                    case ValidationException validationException:
                        problemDetails = new BadRequestProblemDetails(validationException, errorFeature, context);
                        break;
                }

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = problemDetails.Status.GetValueOrDefault(StatusCodes.Status500InternalServerError);
                context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true
                };

                await JsonSerializer.SerializeAsync(context.Response.Body, problemDetails);
            });
        }
    }
}
