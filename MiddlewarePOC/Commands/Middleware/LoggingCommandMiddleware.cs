using Microsoft.Extensions.Logging;

namespace MiddlewarePOC.Commands.Middleware;

public class LoggingCommandMiddleware<TCommand> : ICommandMiddleware<TCommand>
{
	private readonly ILogger _logger;

	public LoggingCommandMiddleware(ILogger<LoggingCommandMiddleware<TCommand>> logger)
	{
		_logger = logger;
	}

	public async Task Handle(TCommand command, CancellationToken cancellationToken, Func<TCommand, CancellationToken, Task> next)
	{
		_logger.LogInformation($"Handling command: {typeof(TCommand).Name}");
		await next(command, cancellationToken);
		_logger.LogInformation($"Handled command: {typeof(TCommand).Name}");
	}
}
