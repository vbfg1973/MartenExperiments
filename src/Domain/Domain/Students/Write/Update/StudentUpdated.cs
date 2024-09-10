namespace Domain.Students.Write.Update
{
    using Core.Events;

    public class StudentUpdated(
        Guid studentId,
        string firstName,
        string lastName)
        : DomainEventBase
    {
        public Guid StudentId { get; init; } = studentId;
        public string FirstName { get; init; } = firstName;
        public string LastName { get; init; } = lastName;
    }
}
