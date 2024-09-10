namespace Domain.Students.Write.Create
{
    using Core.Commands;

    public class CreateStudent(
        string studentReference,
        string familyReference,
        string firstName,
        string lastName)
        : DomainCommandBase
    {
        public Guid StudentId { get; } = Guid.NewGuid();
        public string StudentReference { get; init; } = studentReference;
        public string FamilyReference { get; init; } = familyReference;
        public string FirstName { get; init; } = firstName;
        public string LastName { get; init; } = lastName;
    }
}
