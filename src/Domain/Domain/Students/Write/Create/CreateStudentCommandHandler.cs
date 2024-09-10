namespace Domain.Students.Write.Create
{
    using Core.Aggregates;
    using Core.Commands;
    using Microsoft.Extensions.Logging;

    public class CreateStudentCommandHandler(
        IAggregateRepository<StudentAggregate> repository,
        ILogger<CreateStudentCommandHandler> logger)
        : ICommandHandler<CreateStudent>
    {
        public async Task Handle(CreateStudent command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating Student {StudentReference} with command {AggregateId}",
                command.StudentReference,
                command.StudentId);

            var aggregate = new StudentAggregate(command);

            await repository.Add(command.StudentId, aggregate, cancellationToken);
        }
    }
}