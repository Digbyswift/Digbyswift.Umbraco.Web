#pragma warning disable SA1402
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Website.Controllers;

namespace Digbyswift.Umbraco.Web.Controllers;

public abstract class BaseSurfaceController : SurfaceController
{
    protected ILogger Logger { get; }
    protected IViewRenderer ViewRenderer { get; }

    protected BaseSurfaceController(SurfaceControllerDependencies defaultDependencies) : base(
        defaultDependencies.UmbracoContextAccessor,
        defaultDependencies.DatabaseFactory,
        defaultDependencies.Services,
        defaultDependencies.AppCaches,
        defaultDependencies.ProfilingLogger,
        defaultDependencies.PublishedUrlProvider)
    {
        Logger = defaultDependencies.Logger;
        ViewRenderer = defaultDependencies.ViewRenderer;
    }
}

public abstract class BaseSurfaceController<T> : BaseSurfaceController where T : class, IPublishedContent
{
    protected T? TypedCurrentPage => CurrentPage as T;

    protected BaseSurfaceController(SurfaceControllerDependencies defaultDependencies) : base(defaultDependencies)
    {
    }
}
