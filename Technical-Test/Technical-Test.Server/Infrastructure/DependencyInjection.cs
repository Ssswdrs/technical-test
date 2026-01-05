using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        var assembly = typeof(IProductService).Assembly;

        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));

        foreach (var impl in serviceTypes)
        {
            var iface = impl.GetInterface($"I{impl.Name}");
            if (iface != null)
            {
                services.AddScoped(iface, impl);
            }
        }

        return services;
    }
}
