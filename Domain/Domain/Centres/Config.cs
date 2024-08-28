namespace Domain.Centres
{
    using Core.Aggregates;
    using Core.Commands;
    using Core.Marten;
    using Marten;
    using Marten.Events.Projections;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Read;
    using Read.CentreSummary;
    using Write.Create;
    using Write.Update;

    public static class Config
    {
        public static IServiceCollection AddCentresModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IAggregateRepository<CentreAggregate>, AggregateRepository<CentreAggregate>>()
                .AddCommandHandler<CreateCentre, CreateCentreCommandHandler>()
                .AddCommandHandler<UpdateCentre, UpdateCentreCommandHandler>()
                ;

            return services;
        }

        internal static void ConfigureCentresModule(this StoreOptions options)
        {
            Console.WriteLine("ConfigureCentresModule");
            options.Events.AddEventType(typeof(CentreCreated));
            options.Events.AddEventType(typeof(CentreUpdated));

            options.Projections.Add<CentreSummaryProjection>(ProjectionLifecycle.Inline);
        }
    }
}
