using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace Perks.Web.Tests
{
    /// <summary>
    /// The simple dumb implementation of <see cref="HttpContextBase"/>, handy in unit tests.
    /// </summary>
    public class SimpleHttpContext : HttpContextBase
    {
        private readonly HttpRequestBase _request;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleHttpContext"/> class.
        /// </summary>
        /// <param name="url">The URL of the request.</param>
        public SimpleHttpContext(string url)
        {
            Ensure.ArgumentNotNull(url, "url");

            _request = new SimpleHttpRequest(url);
        }

        /// <summary>
        /// Gets the <see cref="T:System.Web.HttpRequest" /> object for the current HTTP request.
        /// </summary>
        /// <returns>The current HTTP request.</returns>
        public override HttpRequestBase Request
        {
            get { return _request; }
        }

        /// <summary>
        /// Gets or sets security information for the current HTTP request.
        /// </summary>
        /// <returns>An object that contains security information for the current HTTP request.</returns>
        public override IPrincipal User { get; set; }
    }
}
