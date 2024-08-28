namespace Core.Aggregates
{
    using global::Marten;
    using Microsoft.Extensions.Logging;

    public class AggregateRepository<TAggregate>(
        IDocumentSession documentSession,
        ILogger<AggregateRepository<TAggregate>> logger): IAggregateRepository<TAggregate>
        where TAggregate : Aggregate
    {
        public Task<TAggregate?> Find(Guid id, CancellationToken ct)
        {
            return documentSession.Events.AggregateStreamAsync<TAggregate>(id, token: ct);
        }

        public async Task<long> Add(Guid id, TAggregate aggregate, CancellationToken ct = default)
        {
            var events = aggregate.DequeueUncommittedEvents();

            logger.LogDebug("Dequeued {AggregateEventCount} events on add for {AggregateId} type {AggregateType}",
                events.Length, id, typeof(TAggregate).Name);

            documentSession.Events.StartStream<Aggregate>(
                id,
                events
            );

            await documentSession.SaveChangesAsync(ct).ConfigureAwait(false);

            logger.LogDebug("AggregateVersion for {AggregateId} - {AggregateType} is {AggregateVersion} after add",
                id,
                typeof(TAggregate).Name,
                events.Length);

            return events.Length;
        }

        public async Task<long> Update(Guid id, TAggregate aggregate, long? expectedVersion = null,
            CancellationToken ct = default)
        {
            var events = aggregate.DequeueUncommittedEvents();

            logger.LogDebug("Dequeued {AggregateEventCount} events on update for {AggregateId} type {AggregateType}",
                events.Length,
                id,
                typeof(TAggregate).Name);

            var nextVersion = expectedVersion ?? aggregate.Version;

            documentSession.Events.Append(
                id,
                nextVersion,
                events
            );

            logger.LogDebug("AggregateVersion for {AggregateId} - {AggregateType} is {AggregateVersion} after update",
                id,
                typeof(TAggregate).Name,
                nextVersion);

            await documentSession.SaveChangesAsync(ct).ConfigureAwait(false);

            return nextVersion;
        }

        public Task<long> Delete(Guid id, TAggregate aggregate, long? expectedVersion = null,
            CancellationToken ct = default)
        {
            return Update(id, aggregate, expectedVersion, ct);
        }
    }
}
