using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Abstractions
{
    public interface IRequestPostProcessor<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task Process(TRequest request, TResponse response, CancellationToken cancellationToken = default);
    }
}
