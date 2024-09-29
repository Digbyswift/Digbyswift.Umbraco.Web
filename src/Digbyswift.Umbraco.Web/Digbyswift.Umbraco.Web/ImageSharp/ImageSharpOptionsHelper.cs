using SixLabors.ImageSharp.Web.Middleware;

namespace Digbyswift.Umbraco.Web.ImageSharp;

public static class ImageSharpOptionsHelper
{
    public static Task ParseCommandsAsync(ImageCommandContext context)
    {
        context
            .EnsurePermittedCommands()
            .EnsureValidWidthCommand()
            .EnsureValidHeightCommand()
            .EnsureValidQualityCommand(minQuality: 70)
            .EnsureValidFormatCommand();

        return Task.CompletedTask;
    }
}