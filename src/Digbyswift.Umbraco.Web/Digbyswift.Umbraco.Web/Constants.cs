namespace Digbyswift.Umbraco.Web;

public static class Constants
{
    /// <summary>
    /// /umbraco
    /// </summary>
    public const string UmbracoRootPath = "/umbraco/";

    /// <summary>
    /// /app_plugins
    /// </summary>
    public const string UmbracoPluginRootPath = "/app_plugins/";

    /// <summary>
    /// /media
    /// </summary>
    public const string UmbracoMediaPath = "/media/";

    /// <summary>
    /// /install
    /// </summary>
    public const string UmbracoInstallPath = "/install/";

    /// <summary>
    /// /umbraco, /app_plugins, /install, /sb
    /// </summary>
    public static readonly string[] UmbracoReservedPaths = 
    {
        UmbracoRootPath,
        UmbracoPluginRootPath,
        UmbracoInstallPath,
        UmbracoMediaPath,
        "/base",
        "/sb",
    };
}