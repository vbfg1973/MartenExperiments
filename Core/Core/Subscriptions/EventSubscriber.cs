namespace Core.Subscriptions
{
    using Events;
    using global::Marten;
    using global::Marten.Events.Daemon;
    using global::Marten.Events.Daemon.Internals;
    using global::Marten.Subscriptions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class EventSubscriber(IServiceProvider serviceProvider, ILogger<EventSubscriber> logger): SubscriptionBase
    {
        public override async Task<IChangeListener> ProcessEventsAsync(
            EventRange eventRange,
            ISubscriptionController subscriptionController,
            IDocumentOperations operations,
            CancellationToken cancellationToken)
        {
            var lastProcessed = eventRange.SequenceFloor;

            try
            {
                foreach (var @event in eventRange.Events)
                {
                    logger.LogDebug("Subscription collected event type {EventType} with id {EventId}",
                        @event.GetType().Name, @event.Id);

                    using var scope = serviceProvider.CreateScope();
                    var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

                    var eventMetadata = new EventMetadata(
                        @event.Id,
                        (ulong)@event.Version,
                        (ulong)@event.Sequence
                    );

                    await eventBus.Publish(EventEnvelope.From(@event.Data, eventMetadata), cancellationToken)
                        .ConfigureAwait(false);
                }

                return NullChangeListener.Instance;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing Marten subscription: {ExceptionMessage}",
                    ex.Message);
                await subscriptionController.ReportCriticalFailureAsync(ex, lastProcessed).ConfigureAwait(false);

                throw;
            }
        }
    }
}
