public interface ICommandMiddleware<TCommand>
{
	Task Handle(TCommand command, CancellationToken cancellationToken, Func<TCommand, CancellationToken, Task> next);
}
