namespace Core.Aggregates
{
    public interface IAggregateRepository
    {
        Task StoreAsync(AggregateBase aggregate, CancellationToken ct = default);

        Task<T> LoadAsync<T>(Guid id, int? version = null, CancellationToken ct = default)
            where T : AggregateBase;
    }
}
