namespace Domain.Students.Write.Create
{
    using Core.Events;

    public class StudentCreated(
        Guid studentId,
        string studentReference,
        string familyReference,
        string firstName,
        string lastName)
        : DomainEventBase
    {
        public Guid StudentId { get; init; } = studentId;
        public string StudentReference { get; init; } = studentReference;
        public string FamilyReference { get; init; } = familyReference;
        public string FirstName { get; init; } = firstName;
        public string LastName { get; init; } = lastName;
    }
}