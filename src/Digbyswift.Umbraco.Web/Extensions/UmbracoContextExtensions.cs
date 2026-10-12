using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class UmbracoContextExtensions
{
    /// <summary>
    /// Retrieves the content of the closest ancestor which has a domain associated with it.
    /// </summary>
    public static IPublishedContent? GetDomainContent(this IUmbracoContext umbracoContext)
    {
        var domainContentId = umbracoContext.PublishedRequest?.Domain?.ContentId;
        return domainContentId != null ? umbracoContext.Content?.GetById(domainContentId.Value) : null;
    }
}
