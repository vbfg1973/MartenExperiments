namespace Core.Aggregates
{
    public interface IAggregateRepository<T> where T : AggregateBase
    {
        Task<T?> Find(Guid id, CancellationToken cancellationToken);
        Task<long> Add(Guid id, T aggregate, CancellationToken cancellationToken = default);
        Task<long> Update(Guid id, T aggregate, long? expectedVersion = null, CancellationToken cancellationToken = default);
        Task<long> Delete(Guid id, T aggregate, long? expectedVersion = null, CancellationToken cancellationToken = default);
    }
}
