namespace Domain.Centres.Read.CentreSummary
{
    using Core.Queries;
    using Marten;
    using Marten.Pagination;
    using Microsoft.Extensions.Logging;

    public class GetCentreSummariesQueryHandler(IDocumentSession querySession, ILogger<GetCentreSummariesQueryHandler> logger) :
        IQueryHandler<GetCentreSummaries, IPagedList<CentreSummaryReadModel>>
    {
        public Task<IPagedList<CentreSummaryReadModel>> Handle(
            GetCentreSummaries request,
            CancellationToken cancellationToken)
        {
            return querySession.Query<CentreSummaryReadModel>()
                .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
