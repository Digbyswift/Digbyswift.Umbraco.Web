using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.Controllers;

namespace Digbyswift.Umbraco.Web.Controllers
{
    public abstract class BaseVirtualController : UmbracoPageController, IVirtualPageController
    {
        protected readonly ILogger Logger;
        protected readonly ICompositeViewEngine CompositeViewEngine;
        protected readonly IViewRenderer ViewRenderer;

        protected BaseVirtualController(VirtualControllerDependencies defaultDependencies) : base(
            defaultDependencies.Logger,
            defaultDependencies.CompositeViewEngine)
        {
            Logger = defaultDependencies.Logger;
            CompositeViewEngine = defaultDependencies.CompositeViewEngine;
            ViewRenderer = defaultDependencies.ViewRenderer;
        }

        public abstract IPublishedContent FindContent(ActionExecutingContext actionExecutingContext);

        public async Task<string> RenderToStringAsync<TModel>(string viewPath, TModel model)
        {
            return await ViewRenderer.RenderAsStringAsync(this, viewPath, model);
        }
    }

    public abstract class BaseVirtualController<T> : BaseVirtualController where T : class, IPublishedContent
    {
        protected T? TypedCurrentPage => CurrentPage as T;

        protected BaseVirtualController(VirtualControllerDependencies defaultDependencies) : base(defaultDependencies)
        {
        }
    }

}
