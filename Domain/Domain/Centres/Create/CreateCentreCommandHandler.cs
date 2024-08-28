namespace Domain.Centres.Create
{
    using Core.Aggregates;
    using Core.Commands;
    using Microsoft.Extensions.Logging;

    public class CreateCentreCommandHandler(IAggregateRepository repository, ILogger<CreateCentreCommandHandler> logger)
        : ICommandHandler<CreateCentre>
    {
        public async Task Handle(CreateCentre command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating Centre with command {CommandId}", command.Id);

            var aggregate = new CentreAggregate(command);

            await repository.StoreAsync(aggregate, cancellationToken);
        }
    }
}
