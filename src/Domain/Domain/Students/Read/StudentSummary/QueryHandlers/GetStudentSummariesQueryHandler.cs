namespace Domain.Students.Read.StudentSummary.QueryHandlers
{
    using Core.Queries;
    using Marten;
    using Marten.Pagination;
    using Microsoft.Extensions.Logging;
    using Queries;

    public class GetStudentSummariesQueryHandler(IDocumentSession querySession, ILogger<GetStudentSummariesQueryHandler> logger) :
        IQueryHandler<GetStudentSummaries, IPagedList<StudentSummaryReadModel>>
    {
        public Task<IPagedList<StudentSummaryReadModel>> Handle(
            GetStudentSummaries request,
            CancellationToken cancellationToken)
        {
            return querySession.Query<StudentSummaryReadModel>()
                .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}