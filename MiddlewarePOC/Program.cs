using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System.Threading;


try
{
	var serviceProvider = ConfigureServices();
	var commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
	await commandDispatcher.Dispatch(new DoSomethingImportantCommand("handle exceptions"), CancellationToken.None);

	// Assuming you'll also use the query dispatcher at some point
	//var queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
	//var result = await queryDispatcher.Dispatch<MyQuery, MyQueryResult>(new MyQuery(), CancellationToken.None);
}
catch (Exception ex)
{
	Console.ForegroundColor = ConsoleColor.Red;
	Console.WriteLine($"An error occurred: {ex.Message}");
	Console.WriteLine(ex.StackTrace);
	Console.ResetColor();
}

// Resolve and use the dispatcher
var y = 0;
// Assuming you'll also use the query dispatcher at some point
//var queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
//var result = await queryDispatcher.Dispatch<MyQuery, MyQueryResult>(new MyQuery(), CancellationToken.None);

// Add more logic as needed...

IServiceProvider ConfigureServices()
{
	var services = new ServiceCollection();

	// Register command and query handlers
	services.AddTransient<ICommandHandler<DoSomethingImportantCommand>, DoSomethingImportantCommandHandler>();
	//services.AddTransient<IQueryHandler<MyQuery, MyQueryResult>, MyQueryHandler>();

	// Register middlewares
	services.AddTransient<ICommandMiddleware<DoSomethingImportantCommand>, LoggingCommandMiddleware<DoSomethingImportantCommand>>();
	//services.AddTransient<IQueryMiddleware<MyQuery, MyQueryResult>, LoggingQueryMiddleware<MyQuery, MyQueryResult>>();

	// Register dispatchers
	services.AddTransient<ICommandDispatcher, CommandDispatcher>();
	services.AddTransient<IQueryDispatcher, QueryDispatcher>();

	// Register the logging services
	//services.AddLogging(configure => configure.AddConsole());
	services.AddLogging(configure =>
	{
		configure.AddConsole(options =>
		{
			//options.IncludeScopes = true;
			//options.DisableColors = false;
			//options.TimestampFormat = "[HH:mm:ss] ";

			// This line ensures that log messages are written immediately and aren't buffered.
			options.LogToStandardErrorThreshold = LogLevel.None;
		});
		configure.SetMinimumLevel(LogLevel.Trace);
	});
	return services.BuildServiceProvider();
}
