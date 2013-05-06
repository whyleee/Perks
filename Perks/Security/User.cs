using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Perks.Security
{
    /// <summary>
    /// Represents the user in the system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user ID in the system.
        /// </summary>
        /// <value>The user ID in the system.</value>
        public object Id { get; set; }

        /// <summary>
        /// Gets or sets user's login.
        /// </summary>
        /// <returns>User's login.</returns>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user email address.
        /// </summary>
        /// <value>The user email address.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the user was created in the system.
        /// </summary>
        /// <value>The date and time when the user was created in the system.</value>
        public DateTime Created { get; set; }
    }
}
