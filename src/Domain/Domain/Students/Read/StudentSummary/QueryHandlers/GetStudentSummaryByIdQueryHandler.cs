﻿namespace Domain.Students.Read.StudentSummary.QueryHandlers
{
    using Core.Exceptions;
    using Core.Queries;
    using Marten;
    using Queries;

    internal class GetStudentSummaryByIdQueryHandler(IDocumentSession querySession)
        : IQueryHandler<GetStudentSummaryById, StudentSummaryReadModel>
    {
        public async Task<StudentSummaryReadModel> Handle(GetStudentSummaryById request, CancellationToken cancellationToken)
        {
            return await querySession.LoadAsync<StudentSummaryReadModel>(request.StudentId, cancellationToken)
                   ?? throw AggregateNotFoundException.For<StudentSummaryReadModel>(request.StudentId);
        }
    }
}
