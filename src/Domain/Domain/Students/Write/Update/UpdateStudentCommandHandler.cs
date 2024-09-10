namespace Domain.Students.Write.Update
{
    using Core.Aggregates;
    using Core.Commands;
    using Microsoft.Extensions.Logging;

    public class UpdateStudentCommandHandler(
        IAggregateRepository<StudentAggregate> repository,
        ILogger<UpdateStudentCommandHandler> logger)
        : ICommandHandler<UpdateStudent>
    {
        public async Task Handle(UpdateStudent command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Student with command {AggregateId}",
                command.StudentId);

            var aggregate = await repository.GetAndUpdate(
                command.StudentId,
                studentAggregate => studentAggregate.Command(command),
                ct: cancellationToken);
        }
    }
}
