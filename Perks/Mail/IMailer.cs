namespace Perks.Mail
{
    /// <summary>
    /// Represents a service for sending emails.
    /// </summary>
    public interface IMailer
    {
        /// <summary>
        /// Sends a prepared email message.
        /// </summary>
        /// <param name="email">An instance of <see cref="Email"/> class representing the email to send.</param>
        void Send(Email email);
    }
}