namespace Core.Commands
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class InMemoryCommandBus(IServiceProvider serviceProvider, ILogger<InMemoryCommandBus> logger): ICommandBus
    {
        public async Task Send<TCommand>(TCommand command, CancellationToken ct = default)
            where TCommand : DomainCommandBase
        {
            var wasHandled = await TrySend(command, ct).ConfigureAwait(true);

            if (!wasHandled)
            {
                throw new InvalidOperationException($"Unable to find handler for command '{command.GetType().Name}'");
            }
        }

        public async Task<bool> TrySend<TCommand>(TCommand command, CancellationToken ct = default)
            where TCommand : DomainCommandBase
        {
            var commandHandler = serviceProvider.GetService<ICommandHandler<TCommand>>();

            if (commandHandler == null)
            {
                return false;
            }

            logger.LogDebug("Handling command {CommandType} with {CommandHandler}", command.GetType().Name,
                commandHandler.GetType().Name);

            await commandHandler.Handle(command, ct);

            return true;
        }
    }
}
