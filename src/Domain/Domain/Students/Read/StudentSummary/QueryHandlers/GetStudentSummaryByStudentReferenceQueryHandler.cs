namespace Domain.Students.Read.StudentSummary.QueryHandlers
{
    using Core.Queries;
    using Marten;
    using Queries;

    internal class GetStudentSummaryByStudentReferenceQueryHandler(IDocumentSession querySession)
        : IQueryHandler<GetStudentSummaryByStudentReference, StudentSummaryReadModel>
    {
        public async Task<StudentSummaryReadModel> Handle(GetStudentSummaryByStudentReference request, CancellationToken cancellationToken)
        {
            return (await querySession.Query<StudentSummaryReadModel>().FirstOrDefaultAsync(x => x.StudentReference == request.StudentReference, token: cancellationToken))!;
        }
    }
}