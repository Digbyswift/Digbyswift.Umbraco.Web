using Microsoft.AspNetCore.Http;

namespace Digbyswift.Umbraco.Web.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Returns true if the Umbraco Preview cookie is present. This doesn't necessarily
        /// mean that the page being viewed is in preview but it does mean that the current
        /// user has a preview session open. See also <see cref="global::Umbraco.Extensions.HttpRequestExtensions.HasPreviewCookie"/>
        /// </summary>
        public static bool IsPreviewSession(this HttpContext context)
        {
            return context.Request.Cookies.ContainsKey(global::Umbraco.Cms.Core.Constants.Web.PreviewCookieName);
        }

        /// <summary>
        /// Returns true if the Umbraco backoffice auth cookie or installer cookie is present.
        /// This doesn't necessarily mean that the page being viewed is in preview but it does mean that the current
        /// user has a preview session open.
        /// </summary>
        public static bool IsBackOfficeSession(this HttpContext context)
        {
            return context.Request.Cookies.ContainsKey("UMB_UCONTEXT") ||
                   context.Request.Cookies.ContainsKey(global::Umbraco.Cms.Core.Constants.Web.InstallerCookieName);
        }
    }
}