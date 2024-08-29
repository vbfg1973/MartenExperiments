using System.ComponentModel.DataAnnotations;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebApi.Middlewares.ExceptionHandling
{
    public static class ProblemDetailsExtensions
    {
        public static ProblemDetails MapToProblemDetails(this Exception exception)
        {
            var statusCode = exception switch
            {
                ArgumentException _ => StatusCodes.Status400BadRequest,
                ValidationException _ => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException _ => StatusCodes.Status401Unauthorized,
                InvalidOperationException _ => StatusCodes.Status403Forbidden,
                AggregateNotFoundException => StatusCodes.Status404NotFound,
                NotImplementedException _ => StatusCodes.Status501NotImplemented,
                _ => StatusCodes.Status500InternalServerError
            };

            return exception.MapToProblemDetails(statusCode);
        }

        public static ProblemDetails MapToProblemDetails(
            this Exception exception,
            int statusCode,
            string? title = null,
            string? detail = null
        )
        {
            return new ProblemDetails
            {
                Title = title ?? exception.GetType().Name, Detail = detail ?? exception.Message, Status = statusCode
            };
        }
    }
}