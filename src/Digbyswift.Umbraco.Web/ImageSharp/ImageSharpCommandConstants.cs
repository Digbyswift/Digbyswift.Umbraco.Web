namespace Digbyswift.Umbraco.Web.ImageSharp;

public static class ImageSharpCommandConstants
{
    public const string FormatKey = "format";
    public const string QualityKey = "quality";
    public const string ModeKey = "mode";
    public const string WidthKey = "width";
    public const string HeightKey = "height";
    public const string AnchorKey = "anchor";
    public const string VersionKey = "v";
    public const string RndKey = "rnd";

    public static bool IsPermittedKey(string key)
    {
        return GetPermittedKeys().Contains(key);
    }

    private static IEnumerable<string> GetPermittedKeys()
    {
        yield return FormatKey;
        yield return QualityKey;
        yield return ModeKey;
        yield return WidthKey;
        yield return HeightKey;
        yield return AnchorKey;
        yield return VersionKey;
        yield return RndKey;
    }
}