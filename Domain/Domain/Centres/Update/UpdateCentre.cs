namespace Domain.Centres.Update
{
    using Core.Commands;

    public sealed class UpdateCentre(string centreName, string centreCode): DomainCommandBase
    {
        public string Name { get; init; } = centreName;
        public string Code { get; init; } = centreCode;
    }
}
