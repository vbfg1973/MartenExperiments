namespace Core.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : DomainCommandBase
    {
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}