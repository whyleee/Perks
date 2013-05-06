using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Perks.Security;

namespace Perks.Web.Wrappers
{
    /// <summary>
    /// The wrapper for ASP.NET membership services.
    /// </summary>
    public class MembershipWrapper
    {
        /// <summary>
        /// Gets the information from the data source for the specified membership user.
        /// </summary>
        /// <param name="username">The name of the user to retrieve.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object representing the specified user.
        /// If the <paramref name="username"/> parameter does not correspond to an existing user,
        /// this method returns null.
        /// </returns>
        public virtual User GetUser(string username)
        {
            return Membership.GetUser(username).IfNotNull(user => new User
                {
                    Id = user.ProviderUserKey,
                    Name = user.UserName,
                    Email = user.Email,
                    Created = user.CreationDate
                });
        }

        /// <summary>
        /// Verifies that the supplied user name and password are valid.
        /// </summary>
        /// <param name="username">The name of the user to be validated.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <returns><c>true</c> if supplied username and password are valid; otherwise, <c>false</c>.</returns>
        public virtual bool ValidateUser(string username, string password)
        {
            return Membership.ValidateUser(username, password);
        }
    }
}
