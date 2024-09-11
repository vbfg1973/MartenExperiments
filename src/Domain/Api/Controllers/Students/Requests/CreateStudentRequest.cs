namespace Api.Controllers.Students.Requests
{
    public record CreateStudentRequest(
        string FirstName,
        string LastName,
        string StudentReference,
        string FamilyReference);
}
