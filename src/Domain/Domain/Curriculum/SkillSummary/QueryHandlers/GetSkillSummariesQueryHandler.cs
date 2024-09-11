namespace Domain.Curriculum.SkillSummary.QueryHandlers
{
    using Core.Queries;
    using Marten;
    using Marten.Pagination;
    using Microsoft.Extensions.Logging;
    using Queries;

    public class GetSkillSummariesQueryHandler(IDocumentSession querySession, ILogger<GetSkillSummariesQueryHandler> logger) :
        IQueryHandler<GetSkillSummaries, IPagedList<SkillSummaryReadModel>>
    {
        public Task<IPagedList<SkillSummaryReadModel>> Handle(
            GetSkillSummaries request,
            CancellationToken cancellationToken)
        {
            return querySession.Query<SkillSummaryReadModel>()
                .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}