using Umbraco.Cms.Core.Models.Blocks;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class BlockListItemExtensions
{
    public static string TypeAlias(this BlockListItem item)
    {
        return item.Content.ContentType.Alias;
    }

    public static bool Is(this BlockListItem item, string alias)
    {
        return item.TypeAlias().Equals(alias);
    }

    public static bool IsNot(this BlockListItem item, string alias)
    {
        return !item.Is(alias);
    }
}
