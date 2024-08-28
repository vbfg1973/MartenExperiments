namespace Domain.Centres.Update
{
    using Core.Commands;

    public sealed class UpdateCentre(Guid centreId, string centreName, string centreCode): DomainCommandBase
    {
        public Guid CentreId { get; set; } = centreId;
        public string Name { get; init; } = centreName;
        public string Code { get; init; } = centreCode;
    }
}
