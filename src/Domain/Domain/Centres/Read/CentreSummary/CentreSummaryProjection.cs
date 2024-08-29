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
}