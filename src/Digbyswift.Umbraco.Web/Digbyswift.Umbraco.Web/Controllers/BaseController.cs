using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.Controllers;

namespace Digbyswift.Umbraco.Web.Controllers;

public abstract class BaseController : RenderController
{
    protected readonly ILogger Logger;
    protected readonly ICompositeViewEngine CompositeViewEngine;
    protected readonly IViewRenderer ViewRenderer;

    protected BaseController(ControllerDependencies defaultDependencies) : base(
        defaultDependencies.Logger,
        defaultDependencies.CompositeViewEngine,
        defaultDependencies.UmbracoContextAccessor)
    {
        Logger = defaultDependencies.Logger;
        CompositeViewEngine = defaultDependencies.CompositeViewEngine;
        ViewRenderer = defaultDependencies.ViewRenderer;
    }

    [NonAction]
    public async Task<string> RenderToStringAsync<TModel>(string viewPath, TModel model)
    {
        return await ViewRenderer.RenderAsStringAsync(this, viewPath, model);
    }
}
    
public abstract class BaseController<T> : BaseController where T : class, IPublishedContent
{
    protected T? TypedCurrentPage => CurrentPage as T;

    protected BaseController(ControllerDependencies defaultDependencies) : base(defaultDependencies)
    {
    }
}