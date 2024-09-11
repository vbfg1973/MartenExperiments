namespace Domain.Curriculum.Read.SkillSummary.QueryHandlers
{
    using Core.Queries;
    using Marten;
    using Queries;

    internal class GetSkillSummaryBySkillReferenceQueryHandler(IDocumentSession querySession)
        : IQueryHandler<GetSkillSummaryBySkillReference, SkillSummaryReadModel>
    {
        public async Task<SkillSummaryReadModel> Handle(GetSkillSummaryBySkillReference request,
            CancellationToken cancellationToken)
        {
            return (await querySession.Query<SkillSummaryReadModel>()
                .FirstOrDefaultAsync(x => x.SkillReference == request.SkillReference, cancellationToken))!;
        }
    }
}
