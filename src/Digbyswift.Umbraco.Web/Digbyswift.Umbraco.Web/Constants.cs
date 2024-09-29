namespace Digbyswift.Umbraco.Web;

public static class Constants
{
    public const string UmbracoRootPath = "/umbraco/";
    public const string UmbracoPluginRootPath = "/app_plugins/";
    public const string UmbracoMediaPath = "/media/";
    public const string UmbracoInstallPath = "/install/";

    /// <summary>
    /// Gets the following paths:
    /// <list type="bullet">
    /// <item>/umbraco/</item>
    /// <item>/app_plugins/</item>
    /// <item>/media/</item>
    /// <item>/install/</item>
    /// <item>/base</item>
    /// <item>/sb</item>
    /// </list>
    /// </summary>
    public static IEnumerable<string> UmbracoReservedPaths
    {
        get
        {
            yield return UmbracoRootPath;
            yield return UmbracoPluginRootPath;
            yield return UmbracoInstallPath;
            yield return UmbracoMediaPath;
            yield return "/base";
            yield return "/sb";
        }
    }
}
