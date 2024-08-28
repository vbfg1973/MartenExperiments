namespace Domain.Centres
{
    using Core.Aggregates;
    using Write.Create;
    using Write.Update;

    public class CentreAggregate: Aggregate
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

            Enqueue(@event);
            Apply(@event);
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

            Enqueue(@event);
            Apply(@event);
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
