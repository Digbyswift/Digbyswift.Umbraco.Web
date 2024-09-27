using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;

namespace Digbyswift.Umbraco.Web.Controllers;

public class VirtualControllerDependencies
{
    public readonly ILogger<BaseVirtualController> Logger;
    public readonly ICompositeViewEngine CompositeViewEngine;
    public readonly IUmbracoContextAccessor UmbracoContextAccessor;
    public readonly IViewRenderer ViewRenderer;

    public VirtualControllerDependencies(
        ILogger<BaseVirtualController> logger,
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