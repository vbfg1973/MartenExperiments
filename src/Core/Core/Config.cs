namespace Core
{
    using Commands;
    using Microsoft.Extensions.DependencyInjection;
    using Queries;

    public static class Config
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services
                .AddCommandBus()
                .AddQueryBus()
                ;

            return services;
        }
    }
}
