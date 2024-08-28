namespace Core.Queries
{
    using Microsoft.Extensions.DependencyInjection;

    public static class Config
    {
        public static IServiceCollection AddQueryBus(this IServiceCollection services)
        {
            services
                // .AddScoped(sp => new QueryBus(sp))
                .AddScoped<IQueryBus, QueryBus>()
                ;

            return services;
        }

        public static IServiceCollection AddQueryHandler<TQuery, TQueryResult, TQueryHandler>(
            this IServiceCollection services
        )
            where TQuery : notnull
            where TQueryHandler : class, IQueryHandler<TQuery, TQueryResult>
        {
            return services.AddTransient<TQueryHandler>()
                .AddTransient<IQueryHandler<TQuery, TQueryResult>>(sp => sp.GetRequiredService<TQueryHandler>());
        }
    }
}
