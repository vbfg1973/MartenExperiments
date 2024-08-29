namespace Core.Events
{
    public record EventEnvelope<T>(
        T Data,
        EventMetadata Metadata
    ): IEventEnvelope where T : DomainEventBase
    {
        object IEventEnvelope.Data => Data;
    }

    public static class EventEnvelope
    {
        public static IEventEnvelope From(object data, EventMetadata metadata)
        {
            //TODO: Reflection. Urgh! There has to be a better way.
            var type = typeof(EventEnvelope<>).MakeGenericType(data.GetType());
            return (IEventEnvelope)Activator.CreateInstance(type, data, metadata)!;
        }

        public static EventEnvelope<T> From<T>(T data) where T : DomainEventBase
        {
            return new EventEnvelope<T>(data, new EventMetadata(Guid.NewGuid(), 0, 0));
        }
    }
}
