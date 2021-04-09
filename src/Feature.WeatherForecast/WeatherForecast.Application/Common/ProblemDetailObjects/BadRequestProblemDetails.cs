using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using FluentValidation;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace WeatherForecast.Application.Common.ProblemDetailObjects
{
    /// <inheritdoc />
    public class BadRequestProblemDetails : StatusCodeProblemDetails
    {
        public BadRequestProblemDetails(ValidationException ex, IExceptionHandlerFeature errorFeature, HttpContext context)
            : base(StatusCodes.Status400BadRequest)
        {
            const string detail = "One or more validation errors occured";
            string[] errorMessages = ex.Errors.Select(err => err.ErrorMessage).ToArray();
            SetCommonBadRequestProblemDetails(errorFeature, context, detail, errorMessages);
        }

        public BadRequestProblemDetails(ArgumentOutOfRangeException ex, IExceptionHandlerFeature errorFeature, HttpContext context)
            : base(StatusCodes.Status400BadRequest)
        {
            const string detail = "One or more arguments are out of range";
            SetCommonBadRequestProblemDetails(errorFeature, context, detail, ex.Message);
        }

        public BadRequestProblemDetails(InvalidOperationException ex, IExceptionHandlerFeature errorFeature, HttpContext context)
            : base(StatusCodes.Status400BadRequest)
        {
            const string detail = "The requested operation is invalid";
            SetCommonBadRequestProblemDetails(errorFeature, context, detail, ex.Message);
        }

        private void SetCommonBadRequestProblemDetails(IExceptionHandlerFeature errorFeature, HttpContext context, string detail, params string[] errorMessages)
        {
            Detail = detail;
            Instance = errorFeature.GetInstance();
            Extensions.TryAdd("errors", errorMessages);
            Extensions.TryAdd("traceId", Activity.Current?.Id ?? context.TraceIdentifier);
        }
    }
}
