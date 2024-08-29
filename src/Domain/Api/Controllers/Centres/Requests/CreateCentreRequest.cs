namespace Api.Controllers.Centres.Requests
{
    public record CreateCentreRequest(string CentreName, string CentreCode);
    public record UpdateCentreRequest(string CentreName, string CentreCode);
}
