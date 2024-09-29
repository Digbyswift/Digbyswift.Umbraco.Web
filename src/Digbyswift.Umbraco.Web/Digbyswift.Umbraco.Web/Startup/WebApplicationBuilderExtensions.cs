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
    /// <item>SetLocalDb()</item>
    /// </list>
    /// </summary>
    public static IUmbracoBuilder AddCustomUmbracoForAzure(this WebApplicationBuilder services, IWebHostEnvironment env, IConfiguration config)
    {
        var builder = services
            .CreateUmbracoBuilder()
            .AddBackOffice()
            .AddWebsite()
            .AddComposers()
            .AddAzureBlobMediaFileSystem()
            .AddAzureBlobImageSharpCache()
            .AddCdnMediaUrlProvider()
            .SetLocalDb();

        builder.Dashboards()
            .Remove<ContentDashboard>();

        return builder;
    }
}
