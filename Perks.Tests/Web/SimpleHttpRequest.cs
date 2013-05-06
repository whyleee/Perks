using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Perks.Tests.Web
{
    /// <summary>
    /// The simple dumb implementation of <see cref="HttpRequestBase"/>, handy in unit tests.
    /// </summary>
    public class SimpleHttpRequest : HttpRequestBase
    {
        private readonly Uri _url;
        private readonly IDictionary<string, string> _items = new Dictionary<string, string>();
        private readonly NameValueCollection _headers = new NameValueCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleHttpRequest"/> class.
        /// </summary>
        /// <param name="url">The URL of the current HTTP request.</param>
        public SimpleHttpRequest(string url)
        {
            Ensure.ArgumentNotNull(url, "url");

            _url = new Uri(url);
        }

        /// <summary>
        /// Gets the specified object from the <see cref="P:System.Web.HttpRequestBase.Cookies" />,
        /// <see cref="P:System.Web.HttpRequestBase.Form" />,
        /// <see cref="P:System.Web.HttpRequestBase.QueryString" />,
        /// or <see cref="P:System.Web.HttpRequestBase.ServerVariables" /> collections.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The item by the key or <c>null</c> if not found.</returns>
        public override string this[string key]
        {
            get
            {
                return _items.ContainsKey(key) ? _items[key] : null;
            }
        }

        /// <summary>
        /// Gets information about the URL of the current request.
        /// </summary>
        /// <returns>An object that contains information about the URL of the current request.</returns>
        public override Uri Url
        {
            get { return _url; }
        }

        /// <summary>
        /// Gets the collection of HTTP headers that were sent by the client.
        /// </summary>
        /// <returns>The request headers.</returns>
        public override NameValueCollection Headers
        {
            get { return _headers; }
        }
    }
}
