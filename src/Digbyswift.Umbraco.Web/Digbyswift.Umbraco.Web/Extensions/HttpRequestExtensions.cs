using System.Text.RegularExpressions;
using Digbyswift.Core.Constants;
using Microsoft.AspNetCore.Http;

namespace Digbyswift.Umbraco.Web.Extensions
{
    public static class HttpRequestExtensions
    {
        private static readonly Lazy<Regex> PreviewPathRegex = new(() => new Regex(@"^\/(?<id>[\d]{4,})(\/?)$"));

        public static bool IsPreviewPath(this HttpRequest request)
        {
            return PreviewPathRegex.Value.IsMatch(request.Path);
        }

        public static int? GetPreviewId(this HttpRequest request)
        {
            var match = PreviewPathRegex.Value.Match(request.Path);
            if (!match.Success)
                return null;

            return Int32.Parse(match.Groups["id"].Value);
        }

        public static bool TryGetPreviewId(this HttpRequest request, out int? previewId)
        {
            previewId = GetPreviewId(request);
            return previewId != null;
        }
        
        /// <summary>
        /// Returns true if the path is a reserved Umbraco path, / or null
        /// </summary>
        public static bool IsReservedPath(this HttpRequest request)
        {
            if (request.Path.Value is null or StringConstants.ForwardSlash)
                return true;

            foreach (var x in Constants.UmbracoReservedPaths)
            {
                if (request.Path.StartsWithSegments(x)) return true;
            }

            return false;
        }

        public static bool IsMediaPath(this HttpRequest request)
        {
            return request.Path.Value?.StartsWith(Constants.UmbracoMediaPath, StringComparison.CurrentCultureIgnoreCase) ?? false;
        }

    }
}