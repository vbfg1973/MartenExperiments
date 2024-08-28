namespace Core.Queries
{
    using Microsoft.Extensions.DependencyInjection;

    public class QueryBus(IServiceProvider serviceProvider) : IQueryBus
    {
        public Task<TResponse> Query<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : notnull
        {
            var queryHandler =
                serviceProvider.GetService<IQueryHandler<TQuery, TResponse>>()
                ?? throw new InvalidOperationException($"Unable to find handler for Query '{query.GetType().Name}'");

            var queryName = typeof(TQuery).Name;
            var activityName = $"{queryHandler.GetType().Name}/{queryName}";

            return queryHandler.Handle(query, cancellationToken);
        }
    }
}
