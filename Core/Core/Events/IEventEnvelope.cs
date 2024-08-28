namespace Core.Events
{
    public interface IEventEnvelope
    {
        object Data { get; }
        EventMetadata Metadata { get; init; }
    }
}