using Friday.Abstractions;
using Friday.Behaviors;

namespace Friday.Api.Behaviors;

public class LoggingBehavior2<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior2<TRequest, TResponse>> _logger;

    public LoggingBehavior2(ILogger<LoggingBehavior2<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken)
    {
        Console.WriteLine("Handling2 {0}", typeof(TRequest).Name);
        var response = await next();
        Console.WriteLine("Handled2 {0}", typeof(TRequest).Name);
        return response;
    }
}
