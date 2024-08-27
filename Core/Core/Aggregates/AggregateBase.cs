namespace Core.Aggregates
{
    using System.Text.Json.Serialization;

    public abstract class AggregateBase
    {
        [JsonIgnore] private readonly List<object> _uncommittedEvents = new();

        public Guid Id { get; protected set; }

        public long Version { get; protected set; }

        // Get the deltas, i.e. events that make up the state, not yet persisted
        public IEnumerable<object> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        // Mark the deltas as persisted.
        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        protected void AddUncommittedEvent(object @event)
        {
            // add the event to the uncommitted list
            _uncommittedEvents.Add(@event);
        }
    }
}