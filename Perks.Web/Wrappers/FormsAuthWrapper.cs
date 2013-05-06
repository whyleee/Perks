using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Perks.Web.Wrappers
{
    /// <summary>
    /// The wrapper for ASP.NET forms authentication services.
    /// </summary>
    public class FormsAuthWrapper
    {
        /// <summary>
        /// Creates an authentication ticket for the supplied user name and adds it to the cookies
        /// collection of the response, or to the URL if you are using cookieless authentication.
        /// </summary>
        /// <param name="username">The name of an authenticated user.</param>
        /// <param name="rememberMe"><c>true</c> to create a persistent cookie (one that is saved
        /// across browser sessions); otherwise <c>false</c>.</param>
        public virtual void SetAuthCookie(string username, bool rememberMe)
        {
            FormsAuthentication.SetAuthCookie(username, rememberMe);
        }

        /// <summary>
        /// Removes the forms-authentication ticket from the browser.
        /// </summary>
        public virtual void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
