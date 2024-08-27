namespace Core.Commands
{
    public abstract class DomainCommandBase
    {
        protected DomainCommandBase()
        {
        }

        protected DomainCommandBase(Guid aggregateId)
        {
            AggregateId = aggregateId;
            CorrelationId = Id;
            CausationId = Id;
        }

        protected DomainCommandBase(Guid aggregateId, Guid correlationId, Guid causationId)
        {
            AggregateId = aggregateId;
            CorrelationId = correlationId;
            CausationId = causationId;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public Guid AggregateId { get; init; }
        public Guid CorrelationId { get; init; }
        public Guid CausationId { get; init; }

        public DateTimeOffset CreatedUtc { get; set; } = DateTimeOffset.UtcNow;
    }
}
