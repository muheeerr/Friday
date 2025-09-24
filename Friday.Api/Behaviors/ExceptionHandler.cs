using Friday.Abstractions;
using Friday.Behaviors;

namespace Friday.Api.Behaviors;

public class ExceptionHandler<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            throw new ApplicationException($"Error handling {typeof(TRequest).Name}", e);
        }
        
    }
}