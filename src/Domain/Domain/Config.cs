﻿namespace Domain
{
    using Centres;
    using Core.Marten;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Students;

    public static class Config
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .ConfigureMartenServices(configuration, options =>
                {
                    // Projection configs go in here through static methods
                    options.ConfigureCentresModule();
                    options.ConfigureStudentsModule();
                }).Services
                .AddCentresModule(configuration)
                .AddStudentsModule(configuration)
                ;

            return services;
        }
    }
}
