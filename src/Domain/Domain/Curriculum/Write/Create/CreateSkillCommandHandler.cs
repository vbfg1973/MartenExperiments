namespace Domain.Curriculum.Write.Create
{
    using Core.Aggregates;
    using Core.Commands;
    using Microsoft.Extensions.Logging;

    public class CreateSkillCommandHandler(
        IAggregateRepository<SkillAggregate> repository,
        ILogger<CreateSkillCommandHandler> logger)
        : ICommandHandler<CreateSkill>
    {
        public async Task Handle(CreateSkill command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating Skill {SkillReference} with command {AggregateId}",
                command.SkillReference,
                command.SkillId);

            var aggregate = new SkillAggregate(command);

            await repository.Add(command.SkillId, aggregate, cancellationToken);
        }
    }
}