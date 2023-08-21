using Microsoft.Extensions.Logging;

class DoSomethingImportantCommandHandler : ICommandHandler<DoSomethingImportantCommand>
{
	private readonly ILogger _logger;

	public DoSomethingImportantCommandHandler(ILogger<DoSomethingImportantCommand> logger)
	{
		_logger = logger;
	}

		public Task Handle(DoSomethingImportantCommand command, CancellationToken cancellation)
	{
		_logger.LogInformation($"I did something important: {command.Action}");
		return Task.CompletedTask;
	}
}