using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Perks.Mvc
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Determines whether the specified string is null or empty.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified string is null or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty(this IHtmlString source)
        {
            return source == null || source.ToHtmlString().IsNullOrEmpty();
        }

        /// <summary>
        /// Determines whether the specified string is not null or empty.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified string is not null or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNotNullOrEmpty(this IHtmlString source)
        {
            return source != null && source.ToHtmlString().IsNotNullOrEmpty();
        }

        public static IHtmlString ToHtml(this object value)
        {
            if (value is IHtmlString)
            {
                return (IHtmlString) value;
            }

            return value.IfNotNull(x => new HtmlString(x.ToString()));
        }

        public static string ToHtml(this IHtmlString htmlString)
        {
            return htmlString.IfNotNull(x => x.ToHtmlString());
        }

        public static HtmlAttribute Attr(this HtmlHelper html, string name, string value, bool condition = true)
        {
            return new HtmlAttribute(name).Add(value, condition);
        }
    }
}
