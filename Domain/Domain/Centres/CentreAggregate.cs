namespace Domain.Centres
{
    using Core.Aggregates;
    using Create;
    using Update;

    public class CentreAggregate: AggregateBase
    {
        public CentreAggregate()
        {

        }

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

            var @event = new CentreCreated(command.CentreId, command.Name, command.Code);

            Apply(@event);
            Enqueue(@event);
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

            var @event = new CentreUpdated(command.CentreId, command.Name, command.Code);

            Apply(@event);
            Enqueue(@event);
        }

        private void Apply(CentreCreated @event)
        {
            Id = @event.CentreId;
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
