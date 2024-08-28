namespace Core.Queries
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class QueryBus(IServiceProvider serviceProvider, ILogger<QueryBus> logger) : IQueryBus
    {
        public Task<TResponse> Query<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : notnull
        {
            logger.LogDebug("Received query {QueryType} with expected response of {QueryResponse}", typeof(TQuery).Name, typeof(TResponse).Name);

            var queryHandler =
                serviceProvider.GetService<IQueryHandler<TQuery, TResponse>>()
                ?? throw new InvalidOperationException($"Unable to find handler for Query '{query.GetType().Name}'");

            logger.LogDebug("Found QueryHandler {QueryHandlerType} for {QueryType} with expected response of {QueryResponse}",
                queryHandler.GetType().Name,
                typeof(TQuery).Name,
                typeof(TResponse).Name);

            var queryName = typeof(TQuery).Name;
            var activityName = $"{queryHandler.GetType().Name}/{queryName}";

            return queryHandler.Handle(query, cancellationToken);
        }
    }
}
