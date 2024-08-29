using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebApi.Middlewares.ExceptionHandling
{
    public class ExceptionToProblemDetailsHandler(Func<Exception, HttpContext, ProblemDetails?>? customExceptionMap)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            var details = customExceptionMap?.Invoke(exception, httpContext) ?? exception.MapToProblemDetails();

            httpContext.Response.StatusCode = details.Status ?? StatusCodes.Status500InternalServerError;
            await httpContext.Response
                .WriteAsJsonAsync(
                    new ProblemDetails
                    {
                        Title = "An error occurred",
                        Detail = exception.Message,
                        Type = exception.GetType().Name,
                        Status = (int)HttpStatusCode.BadRequest
                    }, cancellationToken).ConfigureAwait(false);

            return true;
        }
    }
}