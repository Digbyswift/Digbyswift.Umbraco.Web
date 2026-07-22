using System.Globalization;
using System.Text.RegularExpressions;
using Digbyswift.Core.Constants;
using Microsoft.AspNetCore.Http;

namespace Digbyswift.Umbraco.Web.Extensions;

public static class HttpRequestExtensions
{
    private static Regex PreviewPathRegex { get; } = new(pattern: @"^\/(?<id>[\d]{4,})(\/?)$", matchTimeout: TimeSpan.FromMilliseconds(150), options: RegexOptions.None);

    public static bool IsPreviewPath(this HttpRequest request)
    {
        return PreviewPathRegex.IsMatch(request.Path);
    }

    public static int? GetPreviewId(this HttpRequest request)
    {
        var match = PreviewPathRegex.Match(request.Path);
        if (!match.Success)
            return null;

        return Int32.Parse(match.Groups["id"].Value, CultureInfo.InvariantCulture);
    }

    public static bool TryGetPreviewId(this HttpRequest request, out int? previewId)
    {
        previewId = GetPreviewId(request);
        return previewId != null;
    }

    /// <summary>
    /// Returns true if the path is a reserved Umbraco path. Returns false if / or null.
    /// </summary>
    public static bool IsReservedPath(this HttpRequest request)
    {
        if (request.Path.Value is null or StringConstants.ForwardSlash)
            return false;

        return Constants.ReservedPaths.Any(x => request.Path.StartsWithSegments(x));
    }

    public static bool IsMediaPath(this HttpRequest request)
    {
        return request.Path.Value?.StartsWith(Constants.MediaPath, StringComparison.CurrentCultureIgnoreCase) ?? false;
    }
}
