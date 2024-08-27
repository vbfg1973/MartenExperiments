namespace Core.Events
{
    public abstract class DomainEventBase
    {
        protected DomainEventBase()
        {
        }

        protected DomainEventBase(Guid aggregateId)
        {
            AggregateId = aggregateId;
            CorrelationId = Id;
            CausationId = Id;
        }

        protected DomainEventBase(Guid aggregateId, Guid correlationId, Guid causationId)
        {
            AggregateId = aggregateId;
            CorrelationId = correlationId;
            CausationId = causationId;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public Guid AggregateId { get; init; }
        public Guid CausationId { get; init; }
        public Guid CorrelationId { get; init; }

        public DateTimeOffset CreatedUtc { get; set; } = DateTimeOffset.UtcNow;
    }
}
