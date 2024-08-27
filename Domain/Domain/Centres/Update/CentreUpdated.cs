namespace Domain.Centres.Update
{
    using Core.Events;

    public sealed class CentreUpdated(string centreName, string centreCode): DomainEventBase
    {
        public string Name { get; init; } = centreName;
        public string Code { get; init; } = centreCode;
    }
}
