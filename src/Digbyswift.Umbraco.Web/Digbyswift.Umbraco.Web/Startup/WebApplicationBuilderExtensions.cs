using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Umbraco.Cms.Core.Dashboards;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;

namespace Digbyswift.Umbraco.Web.Startup;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Performs the following actions and removes redundant features, e.g. welcome dashboard:
    /// <list type="bullet">
    /// <item>CreateUmbracoBuilder()</item>
    /// <item>AddBackOffice()</item>
    /// <item>AddWebsite()</item>
    /// <item>AddComposers()</item>
    /// <item>AddAzureBlobMediaFileSystem()</item>
    /// <item>AddAzureBlobImageSharpCache()</item>
    /// <item>AddCdnMediaUrlProvider()</item>
    /// <item>AddImageSharpCommandParsing()</item>
    /// </list>
    /// Also:
    /// <list type="bullet">
    /// <item>AddConnectionStrings()</item>
    /// <item>AddEnvironmentSettings()</item>
    /// <item>AddEmailSettings()</item>
    /// <item>AddAzureCdnSettings()</item>
    /// <item>AddControllerDependencies()</item>
    /// </list>
    /// </summary>
    public static IUmbracoBuilder AddCustomUmbracoForAzure(this WebApplicationBuilder services, Action<IUmbracoBuilder>? configure = null)
    {
        var builder = services
            .CreateUmbracoBuilder()
            .AddBackOffice()
            .AddWebsite()
            .AddComposers()
            .AddAzureBlobMediaFileSystem()
            .AddAzureBlobImageSharpCache()
            .AddCdnMediaUrlProvider()
            .AddImageSharpCommandParsing()

            // Register settings
            .AddConnectionStrings()
            .AddEnvironmentSettings()
            .AddEmailSettings()
            .AddAzureCdnSettings();

        builder.Dashboards()
            .Remove<ContentDashboard>();

        builder.Services
            .AddControllerDependencies();

        configure?.Invoke(builder);

        return builder;
    }
}
