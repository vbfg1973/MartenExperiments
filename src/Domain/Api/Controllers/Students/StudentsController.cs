namespace Api.Controllers.Students
{
    using Core.Commands;
    using Core.Queries;
    using Domain.Students.Read.StudentSummary;
    using Domain.Students.Read.StudentSummary.Queries;
    using Domain.Students.Write.Create;
    using Domain.Students.Write.Update;
    using Marten.Pagination;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class StudentsController(ICommandBus commandBus, IQueryBus queryBus, ILogger<StudentsController> logger)
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetCentreSummary([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 100)
        {
            var query = GetStudentSummaries.Create(pageNumber, pageSize);
            var result = await queryBus.Query<GetStudentSummaries, IPagedList<StudentSummaryReadModel>>(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentSummary(Guid id)
        {
            var result = await queryBus
                .Query<GetStudentSummaryById, StudentSummaryReadModel>(new GetStudentSummaryById(id));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var result =
                await queryBus.Query<GetStudentSummaryByStudentReference, StudentSummaryReadModel?>(
                    new GetStudentSummaryByStudentReference(request.StudentReference));

            if (result != null)
            {
                return Conflict(new
                {
                    Location = $"api/Students/{result.Id}",
                    Message =
                        $"An existing record with the id '{result.Id}' was already found with student reference '{result.StudentReference}'."
                });
            }

            var command = new CreateStudent(request.StudentReference, request.FamilyReference, request.FirstName,
                request.LastName);

            await commandBus.Send(command);

            return Created($"api/Students/{command.StudentId}", command);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] UpdateStudentRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var command = new UpdateStudent(id, request.FirstName, request.LastName);

            await commandBus.Send(command);

            return NoContent();
        }
    }

    public record CreateStudentRequest(
        string FirstName,
        string LastName,
        string StudentReference,
        string FamilyReference);

    public record UpdateStudentRequest(string FirstName, string LastName);
}
