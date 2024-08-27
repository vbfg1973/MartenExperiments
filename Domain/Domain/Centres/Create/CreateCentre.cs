namespace Domain.Centres.Create
{
    using Core.Commands;

    public sealed class CreateCentre(string centreName, string centreCode): DomainCommandBase
    {
        public string Name { get; init; } = centreName;
        public string Code { get; init; } = centreCode;
    }
}
