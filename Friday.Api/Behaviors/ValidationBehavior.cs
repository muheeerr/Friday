using Friday.Abstractions;
using Friday.Behaviors;

namespace Friday.Api.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken)
    {
        Console.WriteLine("Validating {0}", typeof(TRequest).Name);
        // Example: simple validation - ensure string properties are not null or empty
        foreach (var prop in typeof(TRequest).GetProperties())
        {
            if (prop.PropertyType == typeof(string))
            {
                var val = prop.GetValue(request) as string;
                if (string.IsNullOrWhiteSpace(val))
                {
                    throw new ArgumentException($"Property {prop.Name} cannot be null or empty");
                }
            }
        }

        return await next();
    }
}
