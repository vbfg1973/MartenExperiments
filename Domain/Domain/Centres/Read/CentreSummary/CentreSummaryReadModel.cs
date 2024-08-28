namespace Domain.Centres.Read.CentreSummary
{
    using Marten.Events.Aggregation;
    using Write.Create;
    using Write.Update;

    public class CentreSummaryProjection: SingleStreamProjection<CentreSummaryReadModel>
    {
        public CentreSummaryProjection()
        {
            ProjectEvent<CentreCreated>((model, @event) => model.Apply(@event));
            ProjectEvent<CentreUpdated>((model, @event) => model.Apply(@event));
        }
    }

    public class CentreSummaryReadModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string Code { get; private set; } = null!;

        public void Apply(CentreCreated @event)
        {
            Id = @event.CentreId;
            Name = @event.Name;
            Code = @event.Code;
        }

        public void Apply(CentreUpdated @event)
        {
            Name = @event.Name;
            Code = @event.Code;
        }
    }
}
