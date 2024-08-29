using Core;
using Core.Exceptions;
using Core.Serialization.Newtonsoft;
using Core.WebApi.Middlewares.ExceptionHandling;
using Domain;
using Marten.Exceptions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
    })
    .AddCoreServices()
    .AddDefaultExceptionHandler(
        (exception, _) => exception switch
        {
            AggregateNotFoundException => exception.MapToProblemDetails(StatusCodes.Status404NotFound),
            ConcurrencyException => exception.MapToProblemDetails(StatusCodes.Status412PreconditionFailed),
            InvalidOperationException => exception.MapToProblemDetails(StatusCodes.Status400BadRequest),
            _ => null
        })
    .AddDomainServices(builder.Configuration)
    .AddControllers()
    .AddNewtonsoftJson(opt => opt.SerializerSettings.WithDefaults())
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseExceptionHandler()
    .UseHttpsRedirection()
    .UseRouting()
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    })
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping Carts V1");
        c.RoutePrefix = string.Empty;
    })
    ;

app.Run();
