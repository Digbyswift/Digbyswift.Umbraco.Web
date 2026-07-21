using Umbraco.Cms.Core.Models.PublishedContent;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class PublishedElementExtensions
{
    public static string TypeAlias(this IPublishedElement item)
    {
        return item.ContentType.Alias;
    }

    public static bool Is(this IPublishedElement element, string alias)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentNullException.ThrowIfNull(alias);

        return element.ContentType.Alias.Equals(alias);
    }

    public static bool IsNot(this IPublishedElement element, string alias)
    {
        return !Is(element, alias);
    }

    public static bool IsAny(this IPublishedElement element, params string[] alias)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentNullException.ThrowIfNull(alias);

        return alias.Any(element.Is);
    }
}
