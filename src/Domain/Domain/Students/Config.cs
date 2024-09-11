namespace Domain.Students
{
    using Core.Aggregates;
    using Core.Commands;
    using Core.Queries;
    using Marten;
    using Marten.Events.Projections;
    using Marten.Pagination;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Read.StudentSummary;
    using Read.StudentSummary.Queries;
    using Read.StudentSummary.QueryHandlers;
    using Write.Create;
    using Write.Update;

    public static class Config
    {
        public static IServiceCollection AddStudentsModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddScoped<IAggregateRepository<StudentAggregate>, AggregateRepository<StudentAggregate>>()
                .AddCommandHandlers()
                .AddQueryHandlers()
                ;

            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services
                .AddCommandHandler<CreateStudent, CreateStudentCommandHandler>()
                .AddCommandHandler<UpdateStudent, UpdateStudentCommandHandler>()
                ;

            return services;
        }

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            services
                .AddQueryHandler<GetStudentSummaryByStudentReference, StudentSummaryReadModel, GetStudentSummaryByStudentReferenceQueryHandler>()
                .AddQueryHandler<GetStudentSummaryById, StudentSummaryReadModel, GetStudentSummaryByIdQueryHandler>()
                .AddQueryHandler<GetStudentSummaries, IPagedList<StudentSummaryReadModel>, GetStudentSummariesQueryHandler>()
                ;
            return services;
        }

        internal static void ConfigureStudentsModule(this StoreOptions options)
        {
            options.Events.AddEventType(typeof(StudentCreated));
            options.Events.AddEventType(typeof(StudentUpdated));

            options.Projections.Add<StudentSummaryProjection>(ProjectionLifecycle.Inline);
        }
    }
}
