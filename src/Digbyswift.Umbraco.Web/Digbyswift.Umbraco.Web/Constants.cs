using Umbraco.Cms.Core.Models.PublishedContent;

namespace Digbyswift.Umbraco.Web
{
    public static class Constants
    {
        public const string UmbracoPluginRootPath = "/app_plugins";
        public const string UmbracoRootPath = "/umbraco";

        public static readonly IEnumerable<IPublishedElement> EmptyElementList = Enumerable.Empty<IPublishedElement>();
        public static readonly IEnumerable<IPublishedContent> EmptyContentList = Enumerable.Empty<IPublishedContent>();
    }
}