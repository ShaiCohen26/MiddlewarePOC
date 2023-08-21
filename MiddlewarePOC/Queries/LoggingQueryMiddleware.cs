using Microsoft.Extensions.Logging;

public class LoggingQueryMiddleware<TQuery, TResult> : IQueryMiddleware<TQuery, TResult>
{
	private readonly ILogger _logger;

	public LoggingQueryMiddleware(ILogger<LoggingQueryMiddleware<TQuery, TResult>> logger)
	{
		_logger = logger;
	}

	public async Task<TResult> Handle(TQuery query, CancellationToken cancellationToken, Func<TQuery, CancellationToken, Task<TResult>> next)
	{
		_logger.LogInformation($"Handling query: {typeof(TQuery).Name}");
		var result = await next(query, cancellationToken);
		_logger.LogInformation($"Handled query: {typeof(TQuery).Name}");
		return result;
	}
}
