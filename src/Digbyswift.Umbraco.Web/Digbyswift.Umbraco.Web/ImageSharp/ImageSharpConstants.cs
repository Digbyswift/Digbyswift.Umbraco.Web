namespace Digbyswift.Umbraco.Web.ImageSharp;

public static class ImageSharpConstants
{
    public const string VaryAcceptValue = "Accept";
    public const string VaryKey = "Vary";
    public const string Jpeg = "jpeg";
    public const string Webp = "webp";
    public const string NoFormat = "noformat";

    public const string JpegExtension = ".jpeg";
    public const string JpgExtension = ".jpg";
    public const string GifExtension = ".gif";
    public const string BmpExtension = ".bmp";
    public const string PngExtension = ".png";
    public const string TiffExtension = ".tiff";
    public const string TifExtension = ".tif";

    public const int MaxImageWidth = 2200;
    public const int MaxImageHeight = 2200;

    public static IEnumerable<string> GetSupportedExtensions()
    {
        yield return JpegExtension;
        yield return JpgExtension;
        yield return GifExtension;
        yield return BmpExtension;
        yield return PngExtension;
        yield return TiffExtension;
        yield return TifExtension;
    }
}
