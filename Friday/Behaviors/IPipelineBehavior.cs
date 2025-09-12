using System;
using System.Threading;
using System.Threading.Tasks;
using Friday.Abstractions;

namespace Friday.Behaviors;

public interface IPipelineBehavior<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken);
}