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

    public const int DefaultMaxWidth = 3000;
    public const int DefaultMaxHeight = 3000;
    public const int DefaultMinQuality = 70;

    public static IEnumerable<string> GetSupportedExtensions
    {
        get
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
}
