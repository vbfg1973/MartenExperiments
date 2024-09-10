namespace Domain.Students
{
    using Core.Aggregates;
    using Write.Create;
    using Write.Update;

    public class StudentAggregate: Aggregate
    {
        public StudentAggregate()
        {
        }

        public StudentAggregate(CreateStudent command)
        {
            if (string.IsNullOrEmpty(command.StudentReference))
            {
                throw new ArgumentException(nameof(command.StudentReference));
            }

            if (string.IsNullOrEmpty(command.FamilyReference))
            {
                throw new ArgumentException(nameof(command.FamilyReference));
            }

            if (string.IsNullOrEmpty(command.FirstName))
            {
                throw new ArgumentException(nameof(command.FirstName));
            }

            if (string.IsNullOrEmpty(command.LastName))
            {
                throw new ArgumentException(nameof(command.LastName));
            }

            var @event = new StudentCreated(
                command.StudentId,
                command.StudentReference,
                command.FamilyReference,
                command.FirstName,
                command.LastName);

            Enqueue(@event);
            Apply(@event);
        }

        public string StudentReference { get; private set; } = null!;
        public string FamilyReference { get; private set; } = null!;
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;

        public void Command(UpdateStudent command)
        {
            if (string.IsNullOrEmpty(command.FirstName))
            {
                throw new ArgumentException(nameof(command.FirstName));
            }

            if (string.IsNullOrEmpty(command.LastName))
            {
                throw new ArgumentException(nameof(command.LastName));
            }

            var @event = new StudentUpdated(
                command.StudentId,
                command.FirstName,
                command.LastName);

            Enqueue(@event);
            Apply(@event);
        }

        private void Apply(StudentCreated @event)
        {
            Id = @event.StudentId;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            StudentReference = @event.StudentReference;
            FamilyReference = @event.FamilyReference;

            Version++;
        }

        private void Apply(StudentUpdated @event)
        {
            Id = @event.StudentId;
            FirstName = @event.FirstName;
            LastName = @event.LastName;

            Version++;
        }
    }
}
