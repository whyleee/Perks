using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Perks.Web
{
    /// <summary>
    /// Usefull extensions for <see cref="HttpContext"/>, <see cref="HttpRequest"/> and <see cref="HttpResponse"/> types.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Gets the absolute host URL (including scheme, host and port if it's not 80).
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns>The absolute host URL (including sheme, host and port if it's not 80).</returns>
        public static string GetHostUrl(this HttpRequestBase request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return request.Url.GetComponents(UriComponents.Scheme | UriComponents.Host, UriFormat.Unescaped);
        }

        /// <summary>
        /// Gets the first-level host domain name.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns>The first-level host domain name.</returns>
        public static string GetHostDomain(this HttpRequestBase request)
        {
            Ensure.ArgumentNotNull(request, "request");

            var hostName = request.GetHostUrl();
            var lastDotIndex = hostName.LastIndexOf('.');

            return lastDotIndex != -1 ? hostName.Substring(lastDotIndex + 1) : request.Url.Host;
        }
    }
}
