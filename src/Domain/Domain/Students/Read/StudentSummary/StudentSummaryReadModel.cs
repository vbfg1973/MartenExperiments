namespace Domain.Students.Read.StudentSummary
{
    using Domain.Students.Write.Create;
    using Domain.Students.Write.Update;

    public class StudentSummaryReadModel
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string StudentReference { get; private set; } = null!;
        public string FamilyReference { get; private set; } = null!;

        public void Apply(StudentCreated @event)
        {
            Id = @event.StudentId;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            StudentReference = @event.StudentReference;
            FamilyReference = @event.FamilyReference;
        }

        public void Apply(StudentUpdated @event)
        {
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }
    }
}
