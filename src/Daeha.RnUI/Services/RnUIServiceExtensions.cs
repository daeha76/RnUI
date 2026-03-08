using Microsoft.Extensions.DependencyInjection;
using RnUI.Components.UI.Toast;

namespace RnUI.Services;

/// <summary>
/// Extension methods for registering RnUI services.
/// </summary>
public static class RnUIServiceExtensions
{
    /// <summary>
    /// Registers RnUI services required for overlay components (Dialog, Sheet, DropdownMenu, etc.)
    /// to function correctly, including JS interop support for focus trapping, scroll locking,
    /// and keyboard navigation.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddRnUI(this IServiceCollection services)
    {
        services.AddScoped<RnUIInteropService>();
        services.AddScoped<ToastService>();
        return services;
    }
}
