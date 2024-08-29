namespace Core.Queries
{
    public interface IQueryBus
    {
        Task<TResponse> Query<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : notnull;
    }
}