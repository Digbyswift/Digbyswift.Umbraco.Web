using Digbyswift.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using SixLabors.ImageSharp.Web.Middleware;

namespace Digbyswift.Umbraco.Web.ImageSharp;

public static class ImageCommandContextExtensions
{
    public static ImageCommandContext EnsurePermittedCommands(this ImageCommandContext context)
    {
        var commands = context.Commands.Keys.ToList();
        foreach (var key in commands)
        {
            if (!ImageSharpCommandConstants.IsPermittedKey(key))
                context.Commands.Remove(key);
        }

        return context;
    }

    public static ImageCommandContext EnsureValidQualityCommand(this ImageCommandContext context, int minQuality = 1)
    {
        if (context.Commands.Keys.Contains(ImageSharpCommandConstants.QualityKey))
        {
            var qualityValue = context.Commands[ImageSharpCommandConstants.QualityKey];
            if (!Int32.TryParse(qualityValue, out var quality) || quality < minQuality || quality >= 100)
            {
                context.Commands.Remove(ImageSharpCommandConstants.QualityKey);
            }
        }

        return context;
    }

    /// <summary>
    /// Ensures only Jpeg or WebP format values are allowed.
    /// </summary>
    public static ImageCommandContext EnsureValidFormatCommand(this ImageCommandContext context)
    {
        if (context.Commands.Keys.Contains(ImageSharpCommandConstants.FormatKey))
        {
            var formatValue = context.Commands[ImageSharpCommandConstants.FormatKey];
            if (formatValue != ImageSharpConstants.Jpeg && formatValue != ImageSharpConstants.Webp)
            {
                context.Commands.Remove(ImageSharpCommandConstants.FormatKey);
            }
        }

        if (context.Context.Request.AcceptsWebP() && context.RequiresAutoConversionToWebP())
        {
            context.Commands.Add(ImageSharpCommandConstants.FormatKey, ImageSharpConstants.Webp);
            context.Context.Response.Headers.Append(ImageSharpConstants.VaryKey, HeaderNames.Accept);
        }

        return context;
    }

    public static ImageCommandContext EnsureValidWidthCommand(this ImageCommandContext context)
    {
        if (context.Commands.Keys.Contains(ImageSharpCommandConstants.WidthKey))
        {
            var widthValue = context.Commands[ImageSharpCommandConstants.WidthKey];
            if (!Int32.TryParse(widthValue, out var width) || width > ImageSharpConstants.MaxImageWidth)
            {
                context.Commands.Remove(ImageSharpCommandConstants.WidthKey);
            }
        }

        return context;
    }

    public static ImageCommandContext EnsureValidHeightCommand(this ImageCommandContext context)
    {
        if (context.Commands.Keys.Contains(ImageSharpCommandConstants.HeightKey))
        {
            var heightValue = context.Commands[ImageSharpCommandConstants.HeightKey];
            if (!Int32.TryParse(heightValue, out var height) || height > ImageSharpConstants.MaxImageHeight)
            {
                context.Commands.Remove(ImageSharpCommandConstants.HeightKey);
            }
        }

        return context;
    }

    private static bool RequiresAutoConversionToWebP(this ImageCommandContext context)
    {
        if (!context.Context.Request.Path.HasValue)
            return false;

        if (context.Context.Request.Path.Value.EndsWith(ImageSharpConstants.GifExtension))
            return false;

        if (context.Commands.Contains(ImageSharpCommandConstants.FormatKey))
            return false;

        return true;
    }
}
