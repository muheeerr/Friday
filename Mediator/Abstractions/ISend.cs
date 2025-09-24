using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Abstractions
{
    public interface ISend
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    }
}
