using Microsoft.AspNetCore.Mvc;

namespace Digbyswift.Umbraco.Web.Controllers
{
    public interface IViewRenderer
    {
        Task<string> RenderAsStringAsync<TModel>(Controller controller, string name, TModel model);
        Task<string> RenderAsStringAsync<TModel>(ViewComponent component, string viewName, TModel model);
    }
}
