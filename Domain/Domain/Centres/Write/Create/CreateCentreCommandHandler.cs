namespace Domain.Centres.Write.Create
{
    using Core.Aggregates;
    using Core.Commands;
    using Microsoft.Extensions.Logging;

    public class CreateCentreCommandHandler(IAggregateRepository<CentreAggregate> repository, ILogger<CreateCentreCommandHandler> logger)
        : ICommandHandler<CreateCentre>
    {
        public async Task Handle(CreateCentre command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating Centre with command {AggregateId}", command.CentreId);

            var aggregate = new CentreAggregate(command);

            await repository.Add(command.CentreId, aggregate, cancellationToken);
        }
    }
}
