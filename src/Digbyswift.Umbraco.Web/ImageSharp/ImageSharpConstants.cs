using SixLabors.ImageSharp.Web.Processors;

#pragma warning disable SA1203

namespace Digbyswift.Umbraco.Web.ImageSharp;

public static class ImageSharpConstants
{
    public const int DefaultMaxWidth = 3000;
    public const int DefaultMaxHeight = 3000;
    public const int DefaultMinQuality = 70;

    public const string VaryAcceptValue = "Accept";
    public const string VaryKey = "Vary";

    public const string Png = "png";
    public const string Jpeg = "jpeg";
    public const string Webp = "webp";
    public const string NoFormat = "noformat";
    public static readonly string[] SupportedFormats =
    [
        Png,
        Jpeg,
        Webp,
        NoFormat
    ];

    public const string VersionKey = "v";
    public const string RndKey = "rnd";
    public const string CcKey = "cc";

    public static readonly string[] SupportedKeys =
    [
        FormatWebProcessor.Format,
        QualityWebProcessor.Quality,
        BackgroundColorWebProcessor.Color,
        ResizeWebProcessor.Color,
        ResizeWebProcessor.Compand,
        ResizeWebProcessor.Orient,
        ResizeWebProcessor.Sampler,
        ResizeWebProcessor.Xy,
        ResizeWebProcessor.Mode,
        ResizeWebProcessor.Width,
        ResizeWebProcessor.Height,
        ResizeWebProcessor.Anchor,
        VersionKey,
        RndKey,
        CcKey
    ];
}
