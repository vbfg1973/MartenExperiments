namespace Domain.Curriculum
{
    using Core.Aggregates;
    using Core.Commands;
    using Core.Queries;
    using Marten;
    using Marten.Events.Projections;
    using Marten.Pagination;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SkillSummary;
    using SkillSummary.Queries;
    using SkillSummary.QueryHandlers;
    using SkillSummary.QueryHandlers.Domain.Skills.Read.SkillSummary.QueryHandlers;
    using Write.Create;

    public static class Config
    {
        public static IServiceCollection AddCurriculumModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddScoped<IAggregateRepository<SkillAggregate>, AggregateRepository<SkillAggregate>>()
                .AddCommandHandlers()
                .AddQueryHandlers()
                ;

            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services
                .AddCommandHandler<CreateSkill, CreateSkillCommandHandler>()
                ;

            return services;
        }

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            services
                .AddQueryHandler<GetSkillSummaryById, SkillSummaryReadModel, GetSkillSummaryByIdQueryHandler>()
                .AddQueryHandler<GetSkillSummaries, IPagedList<SkillSummaryReadModel>, GetSkillSummariesQueryHandler>()
                ;

            return services;
        }

        internal static void ConfigureCurriculumModule(this StoreOptions options)
        {
            options.Events.AddEventType(typeof(SkillCreated));

            options.Projections.Add<SkillSummaryProjection>(ProjectionLifecycle.Inline);
        }
    }
}
