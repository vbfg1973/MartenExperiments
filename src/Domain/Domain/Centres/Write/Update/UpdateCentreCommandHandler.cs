namespace Domain.Centres.Write.Update
{
    using Core.Aggregates;
    using Core.Commands;
    using Microsoft.Extensions.Logging;

    public class UpdateCentreCommandHandler(
        IAggregateRepository<CentreAggregate> repository,
        ILogger<UpdateCentreCommandHandler> logger)
        : ICommandHandler<UpdateCentre>
    {
        public async Task Handle(UpdateCentre command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating centre {AggregateId}", command.CentreId);

            var aggregate = await repository.GetAndUpdate(
                command.CentreId,
                centreAggregate => centreAggregate.Command(command),
                ct: cancellationToken);
        }
    }
}
