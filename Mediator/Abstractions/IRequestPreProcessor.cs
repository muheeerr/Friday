using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Abstractions
{
    public interface IRequestPreProcessor<TRequest> where TRequest : IRequest<object>
    {
        Task Process(TRequest request, CancellationToken cancellationToken = default);
    }
}
