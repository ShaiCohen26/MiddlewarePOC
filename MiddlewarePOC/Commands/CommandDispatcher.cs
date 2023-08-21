using Microsoft.Extensions.DependencyInjection;
using MiddlewarePOC.Commands.Common;

class CommandDispatcher : ICommandDispatcher
{
	private readonly IServiceProvider _serviceProvider;

	public CommandDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

	public async Task Dispatch<TCommand>(TCommand command, CancellationToken cancellation)
	{
		var middlewares = _serviceProvider.GetServices<ICommandMiddleware<TCommand>>().ToList();
		middlewares.Reverse();
		Func<TCommand, CancellationToken, Task> next = async (cmd, token) =>
		{
			var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
			await handler.Handle(cmd, token);
		};

		foreach (var middleware in middlewares)
		{
			var previousNext = next;
			next = async (cmd, token) => await middleware.Handle(cmd, token, previousNext);
		}

		await next(command, cancellation);
	}
}
