using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace Digbyswift.Umbraco.Web.Controllers;

public interface IViewRenderer
{
    Task<string> RenderAsStringAsync<TModel>(Controller controller, string name, TModel model);
    Task<string> RenderAsStringAsync<TModel>(ViewComponent component, string name, TModel model);
    Task<HtmlString> RenderAsHtmlStringAsync<TModel>(Controller controller, string name, TModel model);
    Task<HtmlString> RenderAsHtmlStringAsync<TModel>(ViewComponent component, string name, TModel model);
}
