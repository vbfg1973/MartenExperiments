namespace Domain.Centres
{
    using Core.Aggregates;
    using Create;
    using Update;

    public class CentreAggregate : AggregateBase
    {
        public CentreAggregate(string centreName, string centreCode)
        {
            if (string.IsNullOrEmpty(centreName))
            {
                throw new ArgumentNullException(nameof(centreName));
            }

            if (string.IsNullOrEmpty(centreCode))
            {
                throw new ArgumentNullException(nameof(centreCode));
            }

            var @event = new CentreCreated(centreName, centreCode);

            Apply(@event);
            AddUncommittedEvent(@event);
        }

        public string Name { get; private set; }
        public string Code { get; private set; }

        private void Apply(CentreCreated @event)
        {
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