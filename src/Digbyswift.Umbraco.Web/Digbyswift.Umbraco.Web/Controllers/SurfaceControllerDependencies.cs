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
    public IViewRenderer ViewRenderer { get; }
    public ILogger<BaseSurfaceController> Logger { get; }
    public IUmbracoContextAccessor UmbracoContextAccessor { get; }
    public IUmbracoDatabaseFactory DatabaseFactory { get; }
    public ServiceContext Services { get; }
    public AppCaches AppCaches { get; }
    public IProfilingLogger ProfilingLogger { get; }
    public IPublishedUrlProvider PublishedUrlProvider { get; }

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
