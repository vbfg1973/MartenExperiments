namespace Core.Aggregates
{
    public abstract class Aggregate: Aggregate<object, Guid>;

    public abstract class Aggregate<TEvent>: Aggregate<TEvent, Guid> where TEvent : class;

    public abstract class Aggregate<TEvent, TId>: IAggregate<TEvent>
        where TEvent : class
        where TId : notnull
    {
        [NonSerialized] private readonly Queue<TEvent> uncommittedEvents = new();
        public TId Id { get; protected set; } = default!;

        public int Version { get; protected set; }

        public virtual void Apply(TEvent @event) { }

        public object[] DequeueUncommittedEvents()
        {
            var dequeuedEvents = uncommittedEvents.Cast<object>().ToArray();
            ;

            uncommittedEvents.Clear();

            return dequeuedEvents;
        }

        protected void Enqueue(TEvent @event)
        {
            uncommittedEvents.Enqueue(@event);
            Apply(@event);
        }
    }

    public interface IAggregate: IProjection
    {
        int Version { get; }

        object[] DequeueUncommittedEvents();
    }

    public interface IAggregate<in TEvent>: IAggregate, IProjection<TEvent> where TEvent : class;

    public interface IProjection
    {
        void Apply(object @event);
    }

    public interface IProjection<in TEvent>: IProjection where TEvent : class
    {
        void IProjection.Apply(object @event)
        {
            if (@event is TEvent typedEvent)
            {
                Apply(typedEvent);
            }
        }

        void Apply(TEvent @event);
    }
}
