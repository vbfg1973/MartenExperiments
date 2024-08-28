namespace Core.Events
{
    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent @event, CancellationToken ct);
    }

    public class EventHandler<TEvent>(Func<TEvent, CancellationToken, Task> handler)
        : IEventHandler<TEvent> where TEvent : DomainEventBase
    {
        public Task Handle(TEvent @event, CancellationToken ct)
        {
            return handler(@event, ct);
        }
    }
}
