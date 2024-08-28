namespace Api.Controllers.Centres
{
    using Core.Commands;
    using Core.Queries;
    using Domain.Centres.Read.CentreSummary;
    using Domain.Centres.Write.Create;
    using Domain.Centres.Write.Update;
    using Marten.Pagination;
    using Microsoft.AspNetCore.Mvc;
    using Requests;

    [Route("api/[controller]")]
    public class CentresController(ICommandBus commandBus, IQueryBus queryBus, ILogger<CentresController> logger): Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetCentreSummary([FromQuery] int pageNumber,  [FromQuery] int pageSize)
        {
            var query = GetCentreSummaries.Create(pageNumber, pageSize);
            var result = await queryBus.Query<GetCentreSummaries, IPagedList<CentreSummaryReadModel>>(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCentreSummary(Guid id)
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
        public async Task<IActionResult> UpdateCentre([FromRoute] Guid id, [FromBody] CreateCentreRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var command = new UpdateCentre(id, request.CentreName, request.CentreCode);

            await commandBus.Send(command);

            return NoContent();
        }
    }
}
