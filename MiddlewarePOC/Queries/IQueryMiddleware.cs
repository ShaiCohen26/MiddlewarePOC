public interface IQueryMiddleware<TQuery, TResult>
{
	Task<TResult> Handle(TQuery query, CancellationToken cancellationToken, Func<TQuery, CancellationToken, Task<TResult>> next);
}
