namespace Digbyswift.Umbraco.Web;

public static class Constants
{
    public const string BackOfficeRootPath = "/umbraco/";
    public const string PluginRootPath = "/app_plugins/";
    public const string MediaPath = "/media/";
    public const string MiniProfilerPath = "/mini-profiler-resources/";
    public const string InstallPath = "/install/";

    public const string BackOfficeRootVirtualPath = "~/umbraco/";
    public const string PluginRootVirtualPath = "~/app_plugins/";
    public const string MediaVirtualPath = "~/media/";
    public const string MiniProfilerVirtualPath = "~/mini-profiler-resources/";
    public const string InstallVirtualPath = "~/install/";

    /// <summary>
    /// Gets the following paths. See https://docs.umbraco.com/umbraco-cms/13.latest/reference/configuration/globalsettings#reserved-paths for more details:
    /// <list type="bullet">
    /// <item>/umbraco/</item>
    /// <item>/app_plugins/</item>
    /// <item>/media/</item>
    /// <item>/mini-profiler-resources/</item>
    /// <item>/install/</item>
    /// </list>
    /// </summary>
    public static IEnumerable<string> ReservedPaths
    {
        get
        {
            yield return BackOfficeRootPath;
            yield return PluginRootPath;
            yield return InstallPath;
            yield return MiniProfilerPath;
            yield return MediaPath;
        }
    }

    /// <summary>
    /// Gets the following paths. See https://docs.umbraco.com/umbraco-cms/13.latest/reference/configuration/globalsettings#reserved-paths for more details:
    /// <list type="bullet">
    /// <item>~/umbraco/</item>
    /// <item>~/app_plugins/</item>
    /// <item>~/media/</item>
    /// <item>~/mini-profiler-resources/</item>
    /// <item>~/install/</item>
    /// </list>
    /// </summary>
    public static IEnumerable<string> ReservedVirtualPaths
    {
        get
        {
            yield return BackOfficeRootVirtualPath;
            yield return PluginRootVirtualPath;
            yield return InstallVirtualPath;
            yield return MiniProfilerVirtualPath;
            yield return MediaVirtualPath;
        }
    }
}
