#pragma warning disable SA1402
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.Controllers;

namespace Digbyswift.Umbraco.Web.Controllers;

public abstract class BaseVirtualController : UmbracoPageController, IVirtualPageController
{
    protected ILogger Logger { get; }
    protected ICompositeViewEngine CompositeViewEngine { get; }
    protected IViewRenderer ViewRenderer { get; }

    protected BaseVirtualController(VirtualControllerDependencies defaultDependencies) : base(
        defaultDependencies.Logger,
        defaultDependencies.CompositeViewEngine)
    {
        Logger = defaultDependencies.Logger;
        CompositeViewEngine = defaultDependencies.CompositeViewEngine;
        ViewRenderer = defaultDependencies.ViewRenderer;
    }

    public abstract IPublishedContent FindContent(ActionExecutingContext actionExecutingContext);

    [NonAction]
    public async Task<string> RenderToStringAsync<TModel>(string viewPath, TModel model)
    {
        return await ViewRenderer.RenderAsStringAsync(this, viewPath, model);
    }

    [NonAction]
    public async Task<HtmlString> RenderToHtmlStringAsync<TModel>(string viewPath, TModel model)
    {
        return await ViewRenderer.RenderAsHtmlStringAsync(this, viewPath, model);
    }
}

public abstract class BaseVirtualController<T> : BaseVirtualController where T : class, IPublishedContent
{
    protected T? TypedCurrentPage => CurrentPage as T;

    protected BaseVirtualController(VirtualControllerDependencies defaultDependencies) : base(defaultDependencies)
    {
    }
}
