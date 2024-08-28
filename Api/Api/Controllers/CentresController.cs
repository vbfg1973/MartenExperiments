namespace Api.Controllers
{
    using Core.Commands;
    using Domain.Centres.Create;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class CentresController(ICommandBus commandBus, ILogger<CentresController> logger): Controller
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCentre(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCentre([FromBody] CreateCentreRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var command = new CreateCentre(request.CentreName, request.CentreCode);

            await commandBus.Send(command);

            return Created($"api/Centres/{command.Id}", null);
        }

        public record CreateCentreRequest(string CentreName, string CentreCode);
    }
}
