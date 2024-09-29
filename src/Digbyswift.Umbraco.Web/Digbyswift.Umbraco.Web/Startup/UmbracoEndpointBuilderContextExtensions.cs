using Microsoft.AspNetCore.Builder;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using Umbraco.Extensions;

namespace Digbyswift.Umbraco.Web.Startup;

public static class UmbracoEndpointBuilderContextExtensions
{
    public static IUmbracoEndpointBuilderContext MapXmlSitemapRoute(this IUmbracoEndpointBuilderContext builder, string? controllerName = "XmlSitemap", string? actionName = "Index")
    {
        if (!builder.RuntimeState.UmbracoCanBoot())
            return builder;

        builder.EndpointRouteBuilder.MapControllerRoute(
            "Sitemap:Xml",
            "/sitemap.xml",
            new { Controller = controllerName, Action = actionName }
        );

        return builder;
    }
}
