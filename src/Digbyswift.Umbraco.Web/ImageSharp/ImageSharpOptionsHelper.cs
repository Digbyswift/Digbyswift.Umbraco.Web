using SixLabors.ImageSharp.Web.Middleware;

namespace Digbyswift.Umbraco.Web.ImageSharp;

public static class ImageSharpOptionsHelper
{
    public static Task ParseCommandsAsync(ImageCommandContext context, int maxWidth, int maxHeight, int minQuality)
    {
        context
            .EnsurePermittedCommands()
            .EnsureValidDimensions(maxWidth, maxHeight)
            .EnsureValidQuality(minQuality)
            .EnsureValidFormat()
            .CheckForWebPAutoConversion();

        return Task.CompletedTask;
    }
}
