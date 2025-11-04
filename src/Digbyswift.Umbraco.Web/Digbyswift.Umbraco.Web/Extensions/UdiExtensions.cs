using Umbraco.Cms.Core;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class UdiExtensions
{
    public static Guid ToGuid(this Udi value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var guidUdi = value as GuidUdi;
        return guidUdi == null
            ? Guid.Empty
            : guidUdi.Guid;
    }
}
