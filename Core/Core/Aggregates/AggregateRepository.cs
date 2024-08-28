namespace Core.Aggregates
{
    using Marten;
    using Microsoft.Extensions.Logging;

    public class AggregateRepository(IDocumentStore store, ILogger<AggregateRepository> logger): IAggregateRepository
    {
        public async Task StoreAsync(AggregateBase aggregate, CancellationToken ct = default)
        {
            logger.LogDebug("Store aggregate of type {AggregateType} - {AggregateId}",
                aggregate.GetType().Name,
                aggregate.Id);

            await using var session = await store.LightweightSerializableSessionAsync(ct);

            // Take non-persisted events, push them to the event stream, indexed by the aggregate ID
            var events = aggregate.GetUncommittedEvents().ToArray();
            session.Events.Append(aggregate.Id, aggregate.Version, events);
            await session.SaveChangesAsync(ct);

            // Once successfully persisted, clear events from list of uncommitted events
            aggregate.ClearUncommittedEvents();
        }

        public async Task<T> LoadAsync<T>(Guid id, int? version = null, CancellationToken ct = default)
            where T : AggregateBase
        {
            logger.LogDebug("Load aggregate {AggregateId}",
                id);

            await using var session = await store.LightweightSerializableSessionAsync(ct);
            var aggregate = await session.Events.AggregateStreamAsync<T>(id, version ?? 0, token: ct);
            return aggregate ?? throw new InvalidOperationException($"No aggregate by id {id}.");
        }
    }
}
