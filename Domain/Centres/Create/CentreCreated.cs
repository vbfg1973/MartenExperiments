namespace Domain.Centres.Create
{
    using Core.Events;

    public sealed class CentreCreated(string centreName, string centreCode) : DomainEventBase
    {
        public string Name { get; init; } = centreName;
        public string Code { get; init; } = centreCode;
    }
}