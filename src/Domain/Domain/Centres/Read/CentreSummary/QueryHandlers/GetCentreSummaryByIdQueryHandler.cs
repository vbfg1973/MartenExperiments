namespace Domain.Centres.Read.CentreSummary.QueryHandlers
{
    using Core.Exceptions;
    using Core.Queries;
    using Marten;
    using Queries;

    internal class GetCentreSummaryByIdQueryHandler(IDocumentSession querySession)
        : IQueryHandler<GetCentreSummaryById, CentreSummaryReadModel>
    {
        public async Task<CentreSummaryReadModel> Handle(GetCentreSummaryById request, CancellationToken cancellationToken)
        {
            return await querySession.LoadAsync<CentreSummaryReadModel>(request.CentreId, cancellationToken)
                   ?? throw AggregateNotFoundException.For<CentreSummaryReadModel>(request.CentreId);
        }
    }
}