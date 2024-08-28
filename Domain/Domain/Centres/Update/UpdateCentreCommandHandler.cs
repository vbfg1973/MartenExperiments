namespace Domain.Centres.Update
{
    using System.Text.Json;
    using Core.Aggregates;
    using Core.Commands;
    using Microsoft.CodeAnalysis;
    using Microsoft.Extensions.Logging;

    public class UpdateCentreCommandHandler(IAggregateRepository<CentreAggregate> repository, ILogger<UpdateCentreCommandHandler> logger)
        : ICommandHandler<UpdateCentre>
    {
        public async Task Handle(UpdateCentre command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating centre {AggregateId}", command.CentreId);

            var agg = await repository.Get(command.CentreId, cancellationToken: cancellationToken);

            logger.LogInformation("{AggregateBody}", JsonSerializer.Serialize(agg));

            var aggregate = await repository.GetAndUpdate(
                command.CentreId,
                centreAggregate => centreAggregate.Command(command),
                ct:cancellationToken);
        }
    }
}
