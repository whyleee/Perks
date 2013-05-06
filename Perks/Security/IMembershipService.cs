namespace Perks.Security
{
    /// <summary>
    /// Represents the service to work with system users and their roles.
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// Determines whether the specified user exists in the system.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns><c>true</c> if specified user exists in the system; otherwise, <c>false</c>.</returns>
        bool IsUserExists(string username);

        /// <summary>
        /// Verifies that the supplied user name and password are valid.
        /// </summary>
        /// <param name="username">The name of the user to be validated.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <returns><c>true</c> if supplied username and password are valid; otherwise, <c>false</c>.</returns>
        bool IsValidUser(string username, string password);
    }
}