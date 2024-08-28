namespace Domain.Centres.Update
{
    using Core.Aggregates;
    using Core.Commands;
    using Microsoft.Extensions.Logging;

    public class UpdateCentreCommandHandler(IAggregateRepository repository, ILogger<UpdateCentreCommandHandler> logger)
        : ICommandHandler<UpdateCentre>
    {
        public async Task Handle(UpdateCentre command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating centre {AggregateId}", command.AggregateId);

            var aggregate = await repository.LoadAsync<CentreAggregate>(command.AggregateId, ct: cancellationToken);

            aggregate.Command(command);

            await repository.StoreAsync(aggregate, cancellationToken);
        }
    }
}
