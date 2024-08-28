namespace Domain.Centres.Update
{
    using Core.Events;

    public sealed class CentreUpdated(Guid centreId, string centreName, string centreCode): DomainEventBase
    {
        public Guid CentreId { get; set; } = centreId;
        public string Name { get; init; } = centreName;
        public string Code { get; init; } = centreCode;
    }
}
