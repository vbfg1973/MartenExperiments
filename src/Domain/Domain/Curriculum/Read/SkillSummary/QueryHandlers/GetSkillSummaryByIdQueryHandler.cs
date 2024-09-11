namespace Domain.Curriculum.Read.SkillSummary.QueryHandlers
{
    using Core.Exceptions;
    using Core.Queries;
    using Marten;

    namespace Domain.Skills.Read.SkillSummary.QueryHandlers
    {
        using Queries;

        internal class GetSkillSummaryByIdQueryHandler(IDocumentSession querySession)
            : IQueryHandler<GetSkillSummaryById, SkillSummaryReadModel>
        {
            public async Task<SkillSummaryReadModel> Handle(GetSkillSummaryById request, CancellationToken cancellationToken)
            {
                return await querySession.LoadAsync<SkillSummaryReadModel>(request.SkillId, cancellationToken)
                       ?? throw AggregateNotFoundException.For<SkillSummaryReadModel>(request.SkillId);
            }
        }
    }
}
