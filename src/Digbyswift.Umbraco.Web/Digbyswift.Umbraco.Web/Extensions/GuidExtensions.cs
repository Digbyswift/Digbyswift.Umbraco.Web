using Umbraco.Cms.Core;
using uConstants = Umbraco.Cms.Core.Constants;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class GuidExtensions
{
    public static Udi ToUdi(this Guid value, string entityType = uConstants.UdiEntityType.Document)
    {
        return value == Guid.Empty
            ? throw new ArgumentOutOfRangeException(nameof(value), "Cannot be empty")
            : Udi.Create(entityType, value);
    }
}
