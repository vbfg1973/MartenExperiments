namespace Domain.Centres
{
    using Core.Aggregates;
    using Core.Commands;
    using Create;
    using Microsoft.Extensions.DependencyInjection;
    using Update;

    public static class Config
    {
        public static IServiceCollection AddCentresModule(this IServiceCollection services)
        {
            services
                .AddScoped<IAggregateRepository<CentreAggregate>, AggregateRepository<CentreAggregate>>()
                .AddScoped<ICommandHandler<CreateCentre>, CreateCentreCommandHandler>()
                .AddScoped<ICommandHandler<UpdateCentre>, UpdateCentreCommandHandler>()
                ;

            return services;
        }
    }
}
