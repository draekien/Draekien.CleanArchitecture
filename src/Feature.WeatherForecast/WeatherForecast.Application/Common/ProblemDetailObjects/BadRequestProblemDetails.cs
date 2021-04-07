using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using FluentValidation;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Application.Common.ProblemDetailObjects
{
    /// <inheritdoc />
    public class BadRequestProblemDetails : ProblemDetails
    {
        public BadRequestProblemDetails(ValidationException ex, IExceptionHandlerFeature errorFeature, HttpContext context)
        {
            Status = StatusCodes.Status400BadRequest;
            Title = "Bad Request";
            Detail = "One or more validation errors occured";
            Type = "https://httpstatuses.com/400";
            Instance = errorFeature.GetInstance();
            Extensions.TryAdd("errors", ex.Errors.Select(err => err.ErrorMessage));
            Extensions.TryAdd("traceId", Activity.Current?.Id ?? context.TraceIdentifier);
        }

        public BadRequestProblemDetails(ArgumentOutOfRangeException ex, IExceptionHandlerFeature errorFeature, HttpContext context)
        {
            Status = StatusCodes.Status400BadRequest;
            Title = "Bad Request";
            Detail = "One or more arguments are out of range";
            Type = "https://httpstatuses.com/400";
            Instance = errorFeature.GetInstance();
            Extensions.TryAdd("errors", ex.Message);
            Extensions.TryAdd("traceId", Activity.Current?.Id ?? context.TraceIdentifier);
        }

        public BadRequestProblemDetails(InvalidOperationException ex, IExceptionHandlerFeature errorFeature, HttpContext context)
        {
            Status = StatusCodes.Status400BadRequest;
            Title = "Bad Request";
            Detail = "The requested operation is invalid";
            Type = "https://httpstatuses.com/400";
            Instance = errorFeature.GetInstance();
            Extensions.TryAdd("errors", ex.Message);
            Extensions.TryAdd("traceId", Activity.Current?.Id ?? context.TraceIdentifier);
        }
    }
}
