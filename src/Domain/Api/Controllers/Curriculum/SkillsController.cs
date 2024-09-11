namespace Api.Controllers.Curriculum
{
    using Core.Commands;
    using Core.Queries;
    using Domain.Curriculum.SkillSummary;
    using Domain.Curriculum.SkillSummary.Queries;
    using Domain.Curriculum.Write.Create;
    using Marten.Pagination;
    using Microsoft.AspNetCore.Mvc;
    using Requests;

    [Route("api/Curriculum/[controller]")]
    public class SkillsController(ICommandBus commandBus, IQueryBus queryBus, ILogger<SkillsController> logger)
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetCentreSummary([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 100)
        {
            var query = GetSkillSummaries.Create(pageNumber, pageSize);
            var result = await queryBus.Query<GetSkillSummaries, IPagedList<SkillSummaryReadModel>>(query);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkillSummary(Guid id)
        {
            var result = await queryBus
                .Query<GetSkillSummaryById, SkillSummaryReadModel>(new GetSkillSummaryById(id));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] CreateSkillRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var command = new CreateSkill(
                request.SkillId,
                request.SkillName,
                request.SkillReference,
                request.TopicReference,
                request.StrandReference,
                request.SubjectReference,
                request.CurriculumReference);

            await commandBus.Send(command);

            return Created($"api/Curriculum/Skills/{command.SkillId}", command);
        }
    }
}
