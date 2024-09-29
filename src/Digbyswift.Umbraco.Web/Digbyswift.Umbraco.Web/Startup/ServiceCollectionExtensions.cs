using Digbyswift.Umbraco.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Dashboards;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;

namespace Digbyswift.Umbraco.Web.Startup;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Performs the following actions and removes redundant features, e.g. welcome dashboard:
    /// <list type="bullet">
    /// <item>AddUmbraco()</item>
    /// <item>AddBackOffice()</item>
    /// <item>AddWebsite()</item>
    /// <item>AddComposers()</item>
    /// <item>AddRegistrar()</item>
    /// <item>AddAzureBlobMediaFileSystem()</item>
    /// <item>AddAzureBlobImageSharpCache()</item>
    /// <item>AddCdnMediaUrlProvider()</item>
    /// <item>SetLocalDb()</item>
    /// </list>
    /// </summary>
    public static IUmbracoBuilder AddCustomUmbracoForAzure(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
    {
        var builder = services
            .AddUmbraco(env, config)
            .AddBackOffice()
            .AddWebsite()
            .AddComposers()
            .AddRegistrar()
            .AddAzureBlobMediaFileSystem()
            .AddAzureBlobImageSharpCache()
            .AddCdnMediaUrlProvider()
            .SetLocalDb();

        builder.Dashboards()
            .Remove<ContentDashboard>();

        return builder;
    }

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
