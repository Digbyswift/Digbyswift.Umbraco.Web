using Digbyswift.Core.Extensions;
using Microsoft.AspNetCore.Html;
using Umbraco.Cms.Core.Strings;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class HtmlEncodedStringExtensions
{
    public static IHtmlContent ToHtmlContent(this IHtmlEncodedString? value)
    {
        if (value == null)
            return HtmlString.Empty;

        var encodedValue = value.ToHtmlString();
        return !String.IsNullOrWhiteSpace(encodedValue)
            ? new HtmlString(encodedValue)
            : HtmlString.Empty;
    }

    public static string? StripMarkup(this IHtmlEncodedString encodedString)
    {
        var html = encodedString.ToHtmlString();

        return !String.IsNullOrWhiteSpace(html)
            ? html.StripMarkup()
            : html;
    }
}
