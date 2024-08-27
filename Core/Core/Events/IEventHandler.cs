namespace Core.Events
{
    public interface IEventHandler<in TEvent> where TEvent : DomainEventBase
    {
        Task Handle(TEvent @event, CancellationToken ct);
    }
}