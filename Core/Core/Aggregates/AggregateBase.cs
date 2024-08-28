namespace Core.Aggregates
{
    using System.Text.Json.Serialization;

    public abstract class AggregateBase
    {
        [JsonIgnore] private readonly Queue<object> _uncommittedEvents = new();

        public Guid Id { get; protected set; } = default;

        public long Version { get; protected set; }

        public void Enqueue(object @event)
        {
            // add the event to the uncommitted list
            _uncommittedEvents.Enqueue(@event);
        }

        public object[] DequeueUncommittedEvents()
        {
            var dequeuedEvents = _uncommittedEvents.Cast<object>().ToArray();

            _uncommittedEvents.Clear();

            return dequeuedEvents;
        }
    }
}
