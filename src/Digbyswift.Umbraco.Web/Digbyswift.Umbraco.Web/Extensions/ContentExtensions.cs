using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using uConstants = Umbraco.Cms.Core.Constants;

namespace Digbyswift.Umbraco.Web.Extensions
{
    public static class ContentExtensions
    {

        public static Dictionary<string, object?> GetDirtyProperties(this IContent content)
        {
            var dirtyEntries = new Dictionary<string, object?>();

            foreach (var property in content.Properties)
            {
                if (content.IsPropertyDirty(property.Alias))
                {
                    dirtyEntries.Add(property.Alias, property.GetValue());
                }
            }

            return dirtyEntries;
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
}
