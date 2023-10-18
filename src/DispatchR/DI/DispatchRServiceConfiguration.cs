using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public class DispatchRServiceConfiguration
{
    internal List<Assembly> AssembliesToRegister { get; } = new();

    public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;

    public DispatchRServiceConfiguration RegisterServicesFromAssembly(Assembly assembly)
    {
        AssembliesToRegister.Add(assembly);

        return this;
    }
}
