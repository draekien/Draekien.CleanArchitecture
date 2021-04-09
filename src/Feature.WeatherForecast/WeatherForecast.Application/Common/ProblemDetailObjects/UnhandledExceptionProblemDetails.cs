using System;
using System.Collections.Generic;
using System.Diagnostics;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Application.Common.ProblemDetailObjects
{
    /// <inheritdoc />
    public class UnhandledExceptionProblemDetails : StatusCodeProblemDetails
    {
        public UnhandledExceptionProblemDetails(Exception ex, IExceptionHandlerFeature errorFeature, HttpContext context)
            : base(StatusCodes.Status500InternalServerError)
        {
            Detail = "An unhandled exception has occured";
            Instance = errorFeature.GetInstance();
            Extensions.TryAdd("errors", ex.Message);
            Extensions.TryAdd("traceId", Activity.Current?.Id ?? context.TraceIdentifier);
        }
    }
}
