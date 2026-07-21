using Digbyswift.Core.Extensions;
using Digbyswift.Core.Extensions.Validation;
using Digbyswift.Core.Http.Extensions;
using Digbyswift.Umbraco.Web.NotificationHandlers;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using SixLabors.ImageSharp.Web.Middleware;
using SixLabors.ImageSharp.Web.Processors;
using ImgConstants = Digbyswift.Umbraco.Web.ImageSharp.ImageSharpConstants;

namespace Digbyswift.Umbraco.Web.ImageSharp;

public static class ImageCommandContextExtensions
{
    public static ImageCommandContext EnsurePermittedCommands(this ImageCommandContext context)
    {
        var tmpCommands = context.Commands.Keys.ToList();
        foreach (var key in tmpCommands.Where(key => !ImgConstants.SupportedKeys.Contains(key)))
        {
            context.Commands.Remove(key);
        }

        return context;
    }

    internal static ImageCommandContext EnsureValidDimensions(this ImageCommandContext context, int maxWidth, int maxHeight)
    {
        var widthIsValid = true;
        var heightIsValid = true;

        if (context.Commands.TryGetValue(ResizeWebProcessor.Width, out var widthValue) && (!UInt32.TryParse(widthValue, out var width) || width > maxWidth))
        {
            widthIsValid = false;
            context.Commands.Remove(ResizeWebProcessor.Width);
        }

        if (context.Commands.TryGetValue(ResizeWebProcessor.Height, out var heightValue) && (!UInt32.TryParse(heightValue, out var height) || height > maxHeight))
        {
            heightIsValid = false;
            context.Commands.Remove(ResizeWebProcessor.Height);
        }

        if (!widthIsValid || !heightIsValid)
        {
            context.Context.RequestServices.GetService<ILogger<ResizeMediaWhenSavingAsyncHandler>>()?.LogWarning(
                "Image processing dimensions invalid w: {Width}; h: {Height}; image path: {Path}; referrer: {Referrer}; client IP: {Ip} #media",
                widthValue,
                heightValue,
                context.Context.Request.Path.ToString(),
                context.Context.Request.Headers[HeaderNames.Referer].ToString(),
                context.Context.Request.GetClientIp()
            );
        }

        return context;
    }

    internal static ImageCommandContext EnsureValidQuality(this ImageCommandContext context, int minQuality = 1)
    {
        if (!context.Commands.TryGetValue(QualityWebProcessor.Quality, out var qualityValue))
            return context;

        if (UInt32.TryParse(qualityValue, out var quality) && quality >= minQuality && quality <= 100)
            return context;

        context.Commands.Remove(QualityWebProcessor.Quality);

        context.Context.RequestServices.GetService<ILogger<ResizeMediaWhenSavingAsyncHandler>>()?.LogWarning(
            "Image processing quality invalid q: {QualityValue}; image path: {Path}; referrer: {Referrer}; client IP: {Ip} #media",
            qualityValue,
            context.Context.Request.GetDisplayUrl(),
            context.Context.Request.GetRawReferrer(),
            context.Context.Request.GetClientIp()
        );

        return context;
    }

    /// <summary>
    /// Ensures only Jpeg or WebP format values are allowed.
    /// </summary>
    internal static ImageCommandContext EnsureValidFormat(this ImageCommandContext context)
    {
        // If the format key exists and has either no value
        // or an invalid value, then remove the command.
        if (!context.Commands.Contains(FormatWebProcessor.Format))
            return context;

        if (!context.Commands.TryGetValue(FormatWebProcessor.Format, out var format) || String.IsNullOrWhiteSpace(format))
        {
            context.Commands.Remove(FormatWebProcessor.Format);
            return context;
        }

        if (!ImgConstants.SupportedFormats.ContainsIgnoreCase(format))
        {
            context.Commands.Remove(FormatWebProcessor.Format);

            context.Context.RequestServices.GetService<ILogger<ResizeMediaWhenSavingAsyncHandler>>()?.LogWarning(
                "Image processing format invalid f: {Format}; image path: {Path}; referrer: {Referrer}; client IP: {Ip} #media",
                format,
                context.Context.Request.Path.ToString(),
                context.Context.Request.Headers[HeaderNames.Referer].ToString(),
                context.Context.Request.GetClientIp()
            );
        }

        return context;
    }

    /// <summary>
    /// Ensures only Jpeg or WebP format values are allowed.
    /// </summary>
    internal static ImageCommandContext CheckForWebPAutoConversion(this ImageCommandContext context)
    {
        // Should not convert files that are already
        // marked to be formatted.
        if (context.Commands.Contains(FormatWebProcessor.Format))
            return context;

        // Only JPEG and PNG are eligible for automatic conversion.
        var request = context.Context.Request;
        if (!request.IsPngOrJpeg())
            return context;

        // Legacy browsers that don't support WebP.
        if (request.IsInternetExplorer11())
            return context;

        // Default to WebP.
        context.Commands[FormatWebProcessor.Format] = ImgConstants.Webp;

        return context;
    }
}
