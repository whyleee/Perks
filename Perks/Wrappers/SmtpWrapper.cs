using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Perks.Wrappers
{
    /// <summary>
    /// The wrapper for .NET SMTP mail services.
    /// </summary>
    public class SmtpWrapper
    {
        /// <summary>
        /// Sends the specified message to an SMTP server for delivery.
        /// </summary>
        /// <param name="mail">A <see cref="T:System.Net.Mail.MailMessage"/> that contains the message to send.</param>
        public virtual void Send(MailMessage mail)
        {
            new SmtpClient().Send(mail);
        }
    }
}
