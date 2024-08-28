using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.WebApi.Middlewares.ExceptionHandling
{
    public static class ExceptionHandlingMiddleware
    {
        public static IServiceCollection AddDefaultExceptionHandler(
            this IServiceCollection serviceCollection,
            Func<Exception, HttpContext, ProblemDetails?>? customExceptionMap = null
        )
        {
            return serviceCollection
                .AddSingleton<IExceptionHandler>(new ExceptionToProblemDetailsHandler(customExceptionMap))
                .AddProblemDetails();
        }
    }
}
