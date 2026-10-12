using Umbraco.Cms.Core.Models;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class LinkExtensions
{
    public static string? TargetAsAttribute(this Link? link)
    {
        if (link == null)
            return null;

        if (!String.IsNullOrWhiteSpace(link.Target))
            return $"target={link.Target}";

        return link.Type is LinkType.Media or LinkType.External
            ? "target=_blank"
            : null;
    }
}
