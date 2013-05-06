using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Perks.Security;
using Perks.Web.Wrappers;

namespace Perks.Web.Security
{
    /// <summary>
    /// Default ASP.NET membership service.
    /// </summary>
    public class AspNetMembershipService : IMembershipService
    {
        protected readonly MembershipWrapper _membership;

        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetMembershipService"/> class.
        /// </summary>
        public AspNetMembershipService(MembershipWrapper membership)
        {
            Ensure.ArgumentNotNull(membership, "membership");

            _membership = membership;
        }

        /// <summary>
        /// Determines whether the specified user exists in the system.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns><c>true</c> if specified user exists in the system; otherwise, <c>false</c>.</returns>
        public virtual bool IsUserExists(string username)
        {
            return _membership.GetUser(username) != null;
        }

        /// <summary>
        /// Verifies that the supplied user name and password are valid.
        /// </summary>
        /// <param name="username">The name of the user to be validated.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <returns><c>true</c> if supplied username and password are valid; otherwise, <c>false</c>.</returns>
        public virtual bool IsValidUser(string username, string password)
        {
            return _membership.ValidateUser(username, password);
        }
    }
}
