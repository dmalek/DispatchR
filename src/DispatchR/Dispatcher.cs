using DispatchR.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DispatchR;

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public virtual async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic? handler = scope.ServiceProvider.GetRequiredService(handlerType);

        var handleMethod = handlerType?
            .GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.HandleAsync));

        if (handleMethod == null)
        {
            // Handle the case when the handlerType or handleMethod is null
            throw new InvalidOperationException("Invalid handlerType or handleMethod is null.");
        }

        var response = await (Task<TResponse>)handleMethod.Invoke(handler, new object[] { request, cancellationToken });
        return response;
    }

    public virtual async Task SendAsync(IRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
        dynamic? handler = scope.ServiceProvider.GetRequiredService(handlerType);

        var handleMethod = handlerType
              .GetMethod(nameof(IRequestHandler<IRequest>.HandleAsync));

        if (handleMethod == null)
        {
            // Handle the case when the handlerType or handleMethod is null
            throw new InvalidOperationException("Invalid handlerType or handleMethod is null.");
        }

        await (Task)handleMethod.Invoke(handler, new object[] { request, cancellationToken });
    }

    public async Task PublishAsync(INotification notification, CancellationToken cancellationToken = default)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());
        IEnumerable<dynamic>? subscribers = scope.ServiceProvider.GetServices(handlerType);

        foreach (var handler in subscribers)
        {
            var handleMethod = handlerType
             .GetMethod(nameof(INotificationHandler<INotification>.ReceiveAsync));

            if (handleMethod == null)
            {
                // Handle the case when the handlerType or handleMethod is null
                throw new InvalidOperationException("Invalid handlerType or handleMethod is null.");
            }

            await handleMethod.Invoke(handler, new object[] { notification, cancellationToken });
        }
    }
}
