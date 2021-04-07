using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Application.Common.ProblemDetailObjects
{
    public class UnhandledExceptionProblemDetails : ProblemDetails
    {
        public UnhandledExceptionProblemDetails(Exception ex, IExceptionHandlerFeature errorFeature, HttpContext context)
        {
            Status = StatusCodes.Status500InternalServerError;
            Title = "Internal Server Error";
            Detail = "An unhandled exception has occured";
            Type = "https://httpstatuses.com/500";
            Instance = errorFeature.GetInstance();
            Extensions.TryAdd("errors", ex.Message);
            Extensions.TryAdd("traceId", Activity.Current?.Id ?? context.TraceIdentifier);
        }
    }
}
