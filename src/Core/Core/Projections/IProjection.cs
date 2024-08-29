namespace Core.Projections
{
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

    public interface IProjection
    {
        void Apply(object @event);
    }
}
