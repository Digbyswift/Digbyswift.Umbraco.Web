using System.Text.RegularExpressions;
using Digbyswift.Core.Constants;
using Microsoft.AspNetCore.Http;

namespace Digbyswift.Umbraco.Web.Extensions
{
    public static class HttpRequestExtensions
    {

        public static bool IsPreviewPath(this HttpRequest request)
        {
            return Regex.IsMatch(request.Path, @".*\/[\d]+\.aspx");
        }

        public static bool IsReservedPath(this HttpRequest request)
        {
            if (request.Path.Value == null || request.Path.Value == StringConstants.ForwardSlash)
                return true;
            
            return Constants.UmbracoReservedPaths.Any(x => request.Path.StartsWithSegments(x));
        }

        public static bool IsMediaPath(this HttpRequest request)
        {
            return request.Path.Value?.StartsWith("/media/", StringComparison.CurrentCultureIgnoreCase) ?? false;
        }

    }
}