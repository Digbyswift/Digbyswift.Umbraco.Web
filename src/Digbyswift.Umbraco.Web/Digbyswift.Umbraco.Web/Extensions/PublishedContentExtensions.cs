using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class PublishedContentExtensions
{
    public static string TypeAlias(this IPublishedContent item)
    {
        return item.ContentType.Alias;
    }

    public static bool Is(this IPublishedContent content, string alias)
    {
        if (content == null)
            throw new ArgumentNullException(nameof(content));

        if (alias == null)
            throw new ArgumentNullException(nameof(alias));

        return content.ContentType.Alias.Equals(alias);
    }

    public static bool IsAny(this IPublishedContent content, params string[] alias)
    {
        if (content == null)
            throw new ArgumentNullException(nameof(content));

        if (alias == null)
            throw new ArgumentNullException(nameof(alias));

        return alias.Any(content.Is);
    }

    public static bool HasTemplate(this IPublishedContent content)
    {
        if (content == null)
            throw new ArgumentNullException(nameof(content));

        return content.TemplateId > 0;
    }

    public static bool HasAncestor(this IPublishedContent content, string docTypeAlias)
    {
        if (content == null)
            throw new ArgumentNullException(nameof(content));

        return content.Ancestor(docTypeAlias) != null;
    }

    public static IPublishedContent? FirstSibling(this IPublishedContent content)
    {
        return content.Siblings()?.FirstOrDefault();
    }

    public static IPublishedContent? FirstSibling(this IPublishedContent content, string alias)
    {
        return content.Siblings()?.FirstOrDefault(x => x.Is(alias));
    }

    public static T? FirstSibling<T>(this IPublishedContent content) where T : class, IPublishedContent
    {
        return content.Siblings<T>()?.FirstOrDefault();
    }

    public static IPublishedContent? PreviousSibling(this IPublishedContent content)
    {
        return content.PreviousSibling<IPublishedContent>();
    }

    public static IPublishedContent? PreviousSibling(this IPublishedContent content, string alias)
    {
        return content.PreviousSibling<IPublishedContent>(x => x.Is(alias));
    }

    public static T? PreviousSibling<T>(this IPublishedContent content, Func<T, bool>? filter = null) where T : class, IPublishedContent
    {
        if (filter != null)
            return content.Siblings<T>()?.Where(filter).LastOrDefault(x => x.SortOrder < content.SortOrder);

        return content.Siblings<T>()?.LastOrDefault(x => x.SortOrder < content.SortOrder);
    }

    public static IPublishedContent? LastSibling(this IPublishedContent content)
    {
        return content.Siblings()?.LastOrDefault();
    }

    public static IPublishedContent? LastSibling(this IPublishedContent content, string alias)
    {
        return content.Siblings()?.LastOrDefault(x => x.Is(alias));
    }

    public static T? LastSibling<T>(this IPublishedContent content) where T : class, IPublishedContent
    {
        return content.Siblings<T>()?.LastOrDefault();
    }

    public static IPublishedContent? NextSibling(this IPublishedContent content, Func<IPublishedContent, bool>? filter = null)
    {
        return content.NextSibling<IPublishedContent>(filter);
    }

    public static IPublishedContent? NextSibling(this IPublishedContent content, string alias)
    {
        return content.NextSibling(x => x.Is(alias));
    }

    public static T? NextSibling<T>(this IPublishedContent content, Func<T, bool>? filter = null) where T : class, IPublishedContent
    {
        if (filter != null)
            return content.Siblings<T>()?.Where(filter).FirstOrDefault(x => x.SortOrder > content.SortOrder);

        return content.Siblings<T>()?.FirstOrDefault(x => x.SortOrder > content.SortOrder);
    }
}
