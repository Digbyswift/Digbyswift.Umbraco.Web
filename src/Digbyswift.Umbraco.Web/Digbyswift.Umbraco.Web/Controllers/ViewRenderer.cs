using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Digbyswift.Umbraco.Web.Controllers;

public class ViewRenderer : IViewRenderer
{
    private readonly IRazorViewEngine _viewEngine;

    public ViewRenderer(IRazorViewEngine viewEngine) => _viewEngine = viewEngine;

    public async Task<string> RenderAsStringAsync<TModel>(Controller controller, string name, TModel model)
    {
        var viewEngineResult = _viewEngine.FindView(controller.ControllerContext, name, false);
        if (!viewEngineResult.Success)
            throw new InvalidOperationException($"Could not find view: {name}");

        var view = viewEngineResult.View;
        controller.ViewData.Model = model;

        await using var writer = new StringWriter();
        var viewContext = new ViewContext(
            controller.ControllerContext,
            view,
            controller.ViewData,
            controller.TempData,
            writer,
            new HtmlHelperOptions());

        await view.RenderAsync(viewContext);

        return writer.ToString();
    }

    public async Task<HtmlString> RenderAsHtmlStringAsync<TModel>(Controller controller, string name, TModel model)
    {
        var output = await RenderAsStringAsync(controller, name, model);
        return new HtmlString(output);
    }

    public async Task<string> RenderAsStringAsync<TModel>(ViewComponent component, string name, TModel model)
    {
        var viewEngineResult = _viewEngine.FindView(component.ViewContext, name, false);
        if (!viewEngineResult.Success)
            throw new InvalidOperationException($"Could not find view: {name}");

        var view = viewEngineResult.View;
        component.ViewData.Model = model;

        await using var writer = new StringWriter();
        var viewContext = new ViewContext(
            component.ViewContext,
            view,
            component.ViewData,
            component.TempData,
            writer,
            new HtmlHelperOptions());

        await view.RenderAsync(viewContext);

        return writer.ToString();
    }

    public async Task<HtmlString> RenderAsHtmlStringAsync<TModel>(ViewComponent component, string name, TModel model)
    {
        var output = await RenderAsStringAsync(component, name, model);
        return new HtmlString(output);
    }
}
