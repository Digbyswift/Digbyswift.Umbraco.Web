using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using uConstants = Umbraco.Cms.Core.Constants;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class ContentExtensions
{
    public static Dictionary<string, object?> GetDirtyProperties(this IContent content)
    {
        return content.Properties
            .Where(property => content.IsPropertyDirty(property.Alias))
            .ToDictionary(property => property.Alias, property => property.GetValue());
    }

    public static void SetValueAsDocumentUdi(this IContent content, string alias, Guid contentKey)
    {
        content.SetValue(alias, Udi.Create(uConstants.UdiEntityType.Document, contentKey).ToString());
    }

    public static void SetValueAsMediaUdi(this IContent content, string alias, Guid contentKey)
    {
        content.SetValue(alias, Udi.Create(uConstants.UdiEntityType.Media, contentKey).ToString());
    }

    public static void SetValueAsMemberUdi(this IContent content, string alias, Guid contentKey)
    {
        content.SetValue(alias, Udi.Create(uConstants.UdiEntityType.Member, contentKey).ToString());
    }

    public static void SetValueAsElementUdi(this IContent content, string alias, Guid contentKey)
    {
        content.SetValue(alias, Udi.Create(uConstants.UdiEntityType.Element, contentKey).ToString());
    }
}
