using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using Perks.Security;
using Perks.Web.Wrappers;

namespace Perks.Web.Security
{
    /// <summary>
    /// Default ASP.NET authentication service based on forms authentication method.
    /// </summary>
    public class AspNetFormsAuthenticationService : IAuthenticationService
    {
        protected readonly HttpContextBase _httpContext;
        protected readonly IMembershipService _membership;
        protected readonly FormsAuthWrapper _formsAuth;

        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetFormsAuthenticationService"/> class.
        /// </summary>
        public AspNetFormsAuthenticationService(HttpContextBase httpContext, IMembershipService membership, FormsAuthWrapper formsAuth)
        {
            Ensure.ArgumentNotNull(httpContext, "httpContext");
            Ensure.ArgumentNotNull(membership, "membership");
            Ensure.ArgumentNotNull(formsAuth, "formsAuth");

            _httpContext = httpContext;
            _membership = membership;
            _formsAuth = formsAuth;
        }

        /// <summary>
        /// Logs the user into the system.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="rememberMe">If set to <c>true</c>, session will be persistent.</param>
        /// <returns><c>true</c> if login was successful; otherwise, <c>false</c>.</returns>
        public virtual bool LogIn(string username, string password, bool rememberMe)
        {
            if (!_membership.IsValidUser(username, password))
            {
                return false;
            }

            _formsAuth.SetAuthCookie(username, rememberMe);
            return true;
        }

        /// <summary>
        /// Logs out the user from the system.
        /// </summary>
        public virtual void LogOut()
        {
            _formsAuth.SignOut();
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        public virtual IIdentity User
        {
            get { return _httpContext.User.Identity; }
        }
    }
}
