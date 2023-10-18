using DispatchR;
using DispatchR.DI;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddDispatchR(this IServiceCollection services, Action<DispatchRServiceConfiguration> dispachConfig)
    {
        var serviceConfig = new DispatchRServiceConfiguration();
        dispachConfig.Invoke(serviceConfig);
        services.AddDispatchR(serviceConfig);

        return services;
    }


    public static IServiceCollection AddDispatchR(this IServiceCollection services, DispatchRServiceConfiguration dispachConfig)
    {
        services.AddTransient<IDispatcher, Dispatcher>();

        services.AddDispatchRClasses(dispachConfig.AssembliesToRegister.ToArray());
        return services;
    }
}
