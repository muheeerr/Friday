using Friday.Abstractions;
using Friday.Behaviors;

namespace Friday.Api.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken)
    {
        Console.WriteLine("Handling {0}", typeof(TRequest).Name);
        var response = await next();
        Console.WriteLine("Handled {0}", typeof(TRequest).Name);
        return response;
    }
}
