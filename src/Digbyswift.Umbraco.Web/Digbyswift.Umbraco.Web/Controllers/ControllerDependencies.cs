using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;

namespace Digbyswift.Umbraco.Web.Controllers;

public class ControllerDependencies
{
    public ILogger<BaseController> Logger { get; }
    public ICompositeViewEngine CompositeViewEngine { get; }
    public IUmbracoContextAccessor UmbracoContextAccessor { get; }
    public IViewRenderer ViewRenderer { get; }

    public ControllerDependencies(
        ILogger<BaseController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextFactory,
        IViewRenderer viewRenderer)
    {
        Logger = logger;
        CompositeViewEngine = compositeViewEngine;
        UmbracoContextAccessor = umbracoContextFactory;
        ViewRenderer = viewRenderer;
    }
}
