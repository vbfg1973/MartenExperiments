namespace Domain.Centres
{
    using Core.Aggregates;
    using Create;
    using Update;

    public class CentreAggregate: AggregateBase
    {
        public CentreAggregate(CreateCentre command)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                throw new ArgumentNullException(nameof(command.Name));
            }

            if (string.IsNullOrEmpty(command.Code))
            {
                throw new ArgumentNullException(nameof(command.Code));
            }

            var @event = new CentreCreated(command.Name, command.Code);

            Apply(@event);
            AddUncommittedEvent(@event);
        }

        public string Name { get; private set; } = null!;
        public string Code { get; private set; } = null!;

        public void Command(UpdateCentre command)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                throw new ArgumentNullException(nameof(command.Name));
            }

            if (string.IsNullOrEmpty(command.Code))
            {
                throw new ArgumentNullException(nameof(command.Code));
            }

            var @event = new CentreUpdated(command.Name, command.Code);

            Apply(@event);
            AddUncommittedEvent(@event);
        }

        private void Apply(CentreCreated @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            Code = @event.Code;

            Version++;
        }

        private void Apply(CentreUpdated @event)
        {
            Name = @event.Name;
            Code = @event.Code;

            Version++;
        }
    }
}
