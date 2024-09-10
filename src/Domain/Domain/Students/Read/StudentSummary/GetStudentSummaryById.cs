namespace Domain.Students.Read.StudentSummary
{
    public record GetStudentSummaryById(Guid StudentId);
    public record GetStudentSummaryByStudentReference(string StudentReference);
}
