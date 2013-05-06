using System.Security.Principal;

namespace Perks.Security
{
    /// <summary>
    /// Represents the service with authentication API of the system.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Logs the user into the system.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="rememberMe">If set to <c>true</c>, session will be persistent.</param>
        /// <returns><c>true</c> if login was successful; otherwise, <c>false</c>.</returns>
        bool LogIn(string username, string password, bool rememberMe);

        /// <summary>
        /// Logs out the user from the system.
        /// </summary>
        void LogOut();

        /// <summary>
        /// Gets the current user.
        /// </summary>
        IIdentity User { get; }
    }
}