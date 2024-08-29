namespace Core.Events
{
    public interface IEventBus
    {
        Task Publish(IEventEnvelope @event, CancellationToken ct);
    }
}
