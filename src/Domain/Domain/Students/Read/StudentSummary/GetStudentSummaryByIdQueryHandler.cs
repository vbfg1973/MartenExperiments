namespace Domain.Students.Read.StudentSummary
{
    using Core.Exceptions;
    using Core.Queries;
    using Marten;
    using Marten.Pagination;
    using Microsoft.Extensions.Logging;

    internal class GetStudentSummaryByIdQueryHandler(IDocumentSession querySession)
        : IQueryHandler<GetStudentSummaryById, StudentSummaryReadModel>
    {
        public async Task<StudentSummaryReadModel> Handle(GetStudentSummaryById request, CancellationToken cancellationToken)
        {
            return await querySession.LoadAsync<StudentSummaryReadModel>(request.StudentId, cancellationToken)
                   ?? throw AggregateNotFoundException.For<StudentSummaryReadModel>(request.StudentId);
        }
    }

    internal class GetStudentSummaryByStudentReferenceQueryHandler(IDocumentSession querySession)
        : IQueryHandler<GetStudentSummaryByStudentReference, StudentSummaryReadModel>
    {
        public async Task<StudentSummaryReadModel> Handle(GetStudentSummaryByStudentReference request, CancellationToken cancellationToken)
        {
            return (await querySession.Query<StudentSummaryReadModel>().FirstOrDefaultAsync(x => x.StudentReference == request.StudentReference, token: cancellationToken))!;
        }
    }
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
