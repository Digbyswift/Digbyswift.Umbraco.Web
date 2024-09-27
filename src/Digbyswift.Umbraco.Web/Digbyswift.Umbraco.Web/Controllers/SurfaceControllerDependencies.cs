using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;

namespace Digbyswift.Umbraco.Web.Controllers;

public class SurfaceControllerDependencies
{
    public readonly IViewRenderer ViewRenderer;
    public readonly ILogger<BaseSurfaceController> Logger;
    public readonly IUmbracoContextAccessor UmbracoContextAccessor;
    public readonly IUmbracoDatabaseFactory DatabaseFactory;
    public readonly ServiceContext Services;
    public readonly AppCaches AppCaches;
    public readonly IProfilingLogger ProfilingLogger;
    public readonly IPublishedUrlProvider PublishedUrlProvider;

    public SurfaceControllerDependencies(
        ILogger<BaseSurfaceController> logger,
        IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory databaseFactory,
        ServiceContext services,
        AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider,
        IViewRenderer viewRenderer)
    {
        ViewRenderer = viewRenderer;
        Logger = logger;
        UmbracoContextAccessor = umbracoContextAccessor;
        DatabaseFactory = databaseFactory;
        Services = services;
        AppCaches = appCaches;
        ProfilingLogger = profilingLogger;
        PublishedUrlProvider = publishedUrlProvider;
    }
}