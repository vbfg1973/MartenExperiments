namespace Domain.Centres.Read.CentreSummary
{
    using Write.Create;
    using Write.Update;

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
