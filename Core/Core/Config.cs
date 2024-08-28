namespace Core
{
    using Aggregates;
    using Commands;
    using Microsoft.Extensions.DependencyInjection;

    public static class Config
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services
                .AddScoped<IAggregateRepository, AggregateRepository>()
                .AddScoped<ICommandBus, InMemoryCommandBus>()
                ;

            return services;
        }
    }
}
