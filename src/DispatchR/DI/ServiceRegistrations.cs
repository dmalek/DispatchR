using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DispatchR.DI;

public static class ServiceRegistrations
{
    public static IServiceCollection AddDispatchRClasses(this IServiceCollection services, Assembly[] assemblies)
    {
        //add request handlers
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        //add request handlers
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // add notification handlers
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(INotificationHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
