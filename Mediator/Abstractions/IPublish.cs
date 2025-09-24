using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Abstractions
{
    public interface IPublish
    {
        Task Publish(INotification notification, CancellationToken cancellationToken = default);
    }
}
