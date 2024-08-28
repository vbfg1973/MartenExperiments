namespace Core.Events
{
    using System.Collections.Concurrent;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class EventBus(IServiceProvider serviceProvider, ILogger<EventBus> logger): IEventBus
    {
        private static readonly ConcurrentDictionary<Type, MethodInfo> PublishMethods = new();

        public Task Publish(IEventEnvelope eventEnvelope, CancellationToken ct)
        {
            return (Task)GetGenericPublishFor(eventEnvelope)
                .Invoke(this, [eventEnvelope, ct])!;
        }

        private async Task Publish<TEvent>(EventEnvelope<TEvent> eventEnvelope, CancellationToken ct)
            where TEvent : DomainEventBase
        {
            using var scope = serviceProvider.CreateScope();

            var eventName = eventEnvelope.Data.GetType().Name;

            var eventEnvelopeHandlers =
                scope.ServiceProvider.GetServices<IEventHandler<EventEnvelope<TEvent>>>();

            foreach (var eventHandler in eventEnvelopeHandlers)
            {
                var activityName = $"{eventHandler.GetType().Name}/{eventName}";

                await eventHandler.Handle(eventEnvelope, ct);
            }

            // publish also just event data
            // thanks to that both handlers with envelope and without will be called
            var eventHandlers =
                scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

            foreach (var eventHandler in eventHandlers)
            {
                var activityName = $"{eventHandler.GetType().Name}/{eventName}";

                await eventHandler.Handle(eventEnvelope.Data, ct);
            }
        }

        private static MethodInfo GetGenericPublishFor(IEventEnvelope @event)
        {
            return PublishMethods.GetOrAdd(@event.Data.GetType(), eventType =>
                typeof(EventBus)
                    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Single(m => m.Name == nameof(Publish) && m.GetGenericArguments().Any())
                    .MakeGenericMethod(eventType)
            );
        }
    }
}