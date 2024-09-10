namespace Domain.Students.Write.Update
{
    using Core.Commands;

    public class UpdateStudent(
        Guid studentId,
        string firstName,
        string lastName)
        : DomainCommandBase
    {
        public Guid StudentId { get; init; } = studentId;
        public string FirstName { get; init; } = firstName;
        public string LastName { get; init; } = lastName;
    }
}
