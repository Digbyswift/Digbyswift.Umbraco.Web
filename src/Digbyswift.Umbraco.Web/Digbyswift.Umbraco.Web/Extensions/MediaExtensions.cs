using Umbraco.Cms.Core.Models;
using uConstants = Umbraco.Cms.Core.Constants;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class MediaExtensions
{
    public static void SetDimensions(this IMedia media, double? width, double? height)
    {
        if (!media.HasProperty(uConstants.Conventions.Media.Width))
            throw new InvalidOperationException("Media does not have an umbracoWidth property");

        if (width > 0)
            media.SetValue(uConstants.Conventions.Media.Width, width);

        if (height > 0)
            media.SetValue(uConstants.Conventions.Media.Height, height);
    }

    public static void SetBytes(this IMedia media, long bytesLength)
    {
        if (!media.HasProperty(uConstants.Conventions.Media.Bytes))
            throw new InvalidOperationException("Media does not have an umbracoBytes property");

        media.SetValue(uConstants.Conventions.Media.Bytes, bytesLength);
    }
}
