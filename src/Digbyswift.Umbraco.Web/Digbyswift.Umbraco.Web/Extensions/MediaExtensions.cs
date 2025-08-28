using Umbraco.Cms.Core.Models;
using uConstants = Umbraco.Cms.Core.Constants;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class MediaExtensions
{
    public static void SetDimensions(this IMedia media, double? width, double? height)
    {
        if (width > 0)
            media.SetValue(uConstants.Conventions.Media.Width, width);

        if (height > 0)
            media.SetValue(uConstants.Conventions.Media.Height, height);
    }

    public static void SetBytes(this IMedia media, long bytesLength)
    {
        media.SetValue(uConstants.Conventions.Media.Bytes, bytesLength);
    }
}
