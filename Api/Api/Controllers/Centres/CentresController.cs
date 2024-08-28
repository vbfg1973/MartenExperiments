namespace Api.Controllers.Centres
{
    using Core.Commands;
    using Core.Queries;
    using Domain.Centres.Read.CentreSummary;
    using Domain.Centres.Write.Create;
    using Domain.Centres.Write.Update;
    using Microsoft.AspNetCore.Mvc;
    using Requests;

    [Route("api/[controller]")]
    public class CentresController(ICommandBus commandBus, IQueryBus queryBus, ILogger<CentresController> logger): Controller
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCentre(Guid id)
        {
            var result = await queryBus
                .Query<GetCentreSummaryById, CentreSummaryReadModel>(new GetCentreSummaryById(id));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCentre([FromBody] CreateCentreRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var command = new CreateCentre(request.CentreName, request.CentreCode);

            await commandBus.Send(command);

            return Created($"api/Centres/{command.CentreId}", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CreateCentre([FromRoute] Guid id, [FromBody] CreateCentreRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var command = new UpdateCentre(id, request.CentreName, request.CentreCode);

            await commandBus.Send(command);

            return NoContent();
        }
    }
}
