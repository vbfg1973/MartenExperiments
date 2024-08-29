namespace Cli.Features
{
    using Microsoft.Extensions.DependencyInjection;
    using Test;

    public static class Config
    {
        public static IServiceCollection AddVerbs(this IServiceCollection services)
        {
            services.AddScoped<TestVerb>();

            return services;
        }
    }
}
