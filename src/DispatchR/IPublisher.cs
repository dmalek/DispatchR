using DispatchR.Contracts;

namespace DispatchR;

public interface IPublisher
{
    Task PublishAsync(INotification notification, CancellationToken cancellationToken = default);
}
