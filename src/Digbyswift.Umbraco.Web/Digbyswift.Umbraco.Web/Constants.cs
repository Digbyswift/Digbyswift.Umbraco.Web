namespace Digbyswift.Umbraco.Web
{
    public static class Constants
    {
        /// <summary>
        /// /umbraco
        /// </summary>
        public static readonly string UmbracoRootPath = "/umbraco";

        /// <summary>
        /// /app_plugins
        /// </summary>
        public static readonly string UmbracoPluginRootPath = "/app_plugins";

        /// <summary>
        /// /media
        /// </summary>
        public static readonly string UmbracoMediaPath = "/media";

        /// <summary>
        /// /install
        /// </summary>
        public static readonly string UmbracoInstallPath = "/install";

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
}