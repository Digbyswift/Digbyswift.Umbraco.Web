using Microsoft.AspNetCore.Http;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Web;

namespace Digbyswift.Umbraco.Web;

/// <summary>
/// An abstract class for basing a simple repository style collection of shortcuts to published content.
/// </summary>
public abstract class SiteCacheBase : IDisposable
{
    private readonly UmbracoContextReference _contextReference;

    protected readonly IPublishedContentCache? ContentCache;
    protected readonly HttpContext? HttpContext;

    protected SiteCacheBase(IHttpContextAccessor httpContextAccessor, IUmbracoContextFactory contextFactory)
    {
        HttpContext = httpContextAccessor.HttpContext ?? throw new InvalidOperationException("CurrentSiteCache cannot be used when HttpContext is null");

        _contextReference = contextFactory.EnsureUmbracoContext();

        ContentCache = _contextReference.UmbracoContext.Content;
    }

    public IPublishedContent? Get(int id)
    {
        return ContentCache?.GetById(id);
    }

    public IPublishedContent? Get(Udi udi)
    {
        return ContentCache?.GetById(udi);
    }

    public T? Get<T>(int id) where T : class, IPublishedContent
    {
        return Get(id) as T;
    }

    public T? Get<T>(Udi udi) where T : class, IPublishedContent
    {
        return Get(udi) as T;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _contextReference?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}