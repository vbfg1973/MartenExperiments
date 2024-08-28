namespace Domain.Centres.Create
{
    using Core.Events;

    public sealed class CentreCreated(Guid centreId, string centreName, string centreCode): DomainEventBase
    {
        public Guid CentreId { get; set; } = centreId;
        public string Name { get; init; } = centreName;
        public string Code { get; init; } = centreCode;
    }
}
