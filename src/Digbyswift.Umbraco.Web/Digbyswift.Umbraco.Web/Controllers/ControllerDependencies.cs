using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;

namespace Digbyswift.Umbraco.Web.Controllers
{
    public class ControllerDependencies
    {
        public readonly ILogger<BaseController> Logger;
        public readonly ICompositeViewEngine CompositeViewEngine;
        public readonly IUmbracoContextAccessor UmbracoContextAccessor;
        public readonly IViewRenderer ViewRenderer;

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
}
