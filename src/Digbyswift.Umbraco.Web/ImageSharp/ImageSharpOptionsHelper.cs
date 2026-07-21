using SixLabors.ImageSharp.Web.Middleware;

namespace Digbyswift.Umbraco.Web.ImageSharp;

public static class ImageSharpOptionsHelper
{
    public static Task ParseCommandsAsync(ImageCommandContext context, int maxWidth, int maxHeight, int minQuality)
    {
        context
            .EnsurePermittedCommands()
            .EnsureValidWidthCommand(maxWidth)
            .EnsureValidHeightCommand(maxHeight)
            .EnsureValidQualityCommand(minQuality)
            .EnsureValidFormatCommand();

        return Task.CompletedTask;
    }
}
