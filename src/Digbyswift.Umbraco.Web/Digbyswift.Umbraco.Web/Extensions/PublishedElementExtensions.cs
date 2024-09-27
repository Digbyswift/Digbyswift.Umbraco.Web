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
        if (element == null)
            throw new ArgumentNullException(nameof(element));

        if (alias == null)
            throw new ArgumentNullException(nameof(alias));

        return element.ContentType.Alias.Equals(alias);
    }

    public static bool IsAny(this IPublishedElement element, params string[] alias)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element));

        if (alias == null)
            throw new ArgumentNullException(nameof(alias));

        return alias.Any(element.Is);
    }
}