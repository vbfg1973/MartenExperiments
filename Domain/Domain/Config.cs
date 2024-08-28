namespace Domain
{
    using Centres;
    using Microsoft.Extensions.DependencyInjection;

    public static class Config
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services
                .AddCentresModule()
                ;

            return services;
        }
    }
}
