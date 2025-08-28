using Digbyswift.Core.Constants;
using Digbyswift.Umbraco.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Web;

namespace Digbyswift.Umbraco.Web;

/// <summary>
/// An abstract class for basing a simple request-based repository of shortcuts to published content.
/// </summary>
public abstract class SiteRequestCacheBase
{
    public IPublishedContentCache? ContentCache { get; }
    public HttpContext? HttpContext { get; }

    protected SiteRequestCacheBase(IHttpContextAccessor httpContextAccessor, IUmbracoContextFactory contextFactory)
    {
        HttpContext = httpContextAccessor.HttpContext ?? throw new InvalidOperationException("Cannot be used when HttpContext is null");

        // Return early because this can't/shouldn't be used for reserved paths.
        if (HttpContext.Request.IsReservedPath() && HttpContext.Request.Path != StringConstants.ForwardSlash)
            return;

        var contextReference = contextFactory.EnsureUmbracoContext();
        if (contextReference.UmbracoContext == null)
            throw new InvalidOperationException($"Cannot be used when UmbracoContext is null for {HttpContext.Request.GetDisplayUrl()}");

        ContentCache = contextReference.UmbracoContext.Content ?? throw new InvalidOperationException($"Cannot be used when PublishedContentCache is for {HttpContext.Request.GetDisplayUrl()}");
    }

    public IPublishedContent? Get(int id) => ContentCache?.GetById(id);
    public IPublishedContent? Get(Udi udi) => ContentCache?.GetById(udi);
    public IPublishedContent? Get(Guid key) => ContentCache?.GetById(key);
    public T? Get<T>(int id) where T : class, IPublishedContent => Get(id) as T;
    public T? Get<T>(Udi udi) where T : class, IPublishedContent => Get(udi) as T;
    public T? Get<T>(Guid key) where T : class, IPublishedContent => Get(key) as T;
}
