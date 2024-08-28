namespace Domain.Centres
{
    using Core.Aggregates;
    using Core.Commands;
    using Core.Queries;
    using Marten;
    using Marten.Events.Projections;
    using Marten.Pagination;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Read.CentreSummary;
    using Write.Create;
    using Write.Update;

    public static class Config
    {
        public static IServiceCollection AddCentresModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddScoped<IAggregateRepository<CentreAggregate>, AggregateRepository<CentreAggregate>>()
                .AddCommandHandlers()
                .AddQueryHandlers()
                ;

            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services
                .AddCommandHandler<CreateCentre, CreateCentreCommandHandler>()
                .AddCommandHandler<UpdateCentre, UpdateCentreCommandHandler>()
                ;

            return services;
        }

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            services
                .AddQueryHandler<GetCentreSummaryById, CentreSummaryReadModel, GetCentreSummaryByIdQueryHandler>()
                .AddQueryHandler<GetCentreSummaries, IPagedList<CentreSummaryReadModel>, GetCentreSummariesQueryHandler>()
                ;

            return services;
        }

        internal static void ConfigureCentresModule(this StoreOptions options)
        {
            options.Events.AddEventType(typeof(CentreCreated));
            options.Events.AddEventType(typeof(CentreUpdated));

            options.Projections.Add<CentreSummaryProjection>(ProjectionLifecycle.Inline);
        }
    }
}
