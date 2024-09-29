using Digbyswift.Umbraco.Web.Controllers;
using Digbyswift.Umbraco.Web.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Digbyswift.Umbraco.Web.Startup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddControllerDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IViewRenderer, ViewRenderer>();

        services
            .AddScoped<ControllerDependencies>()
            .AddScoped<SurfaceControllerDependencies>()
            .AddScoped<VirtualControllerDependencies>();

        return services;
    }
}
