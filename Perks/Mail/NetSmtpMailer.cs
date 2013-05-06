using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Perks.Wrappers;

namespace Perks.Mail
{
    /// <summary>
    /// Default .NET Framework implementation of SMTP email sender.
    /// </summary>
    public class NetSmtpMailer : IMailer
    {
        protected readonly SmtpWrapper _smtp;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetSmtpMailer"/> class.
        /// </summary>
        public NetSmtpMailer(SmtpWrapper smtp)
        {
            Ensure.ArgumentNotNull(smtp, "smtp");

            _smtp = smtp;
        }

        /// <summary>
        /// Sends the specified message to an SMTP server for delivery.
        /// </summary>
        /// <param name="email">An instance of <see cref="Email" /> class representing the email to send.</param>
        public void Send(Email email)
        {
            Ensure.ArgumentNotNull(email, "email");
            Ensure.ArgumentNotNullOrEmpty(email.From, "from");

            var mail = new MailMessage
            {
                From = new MailAddress(email.From),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = email.IsHtml
            };

            foreach (var address in email.To)
            {
                mail.To.Add(address);
            }

            _smtp.Send(mail);
        }
    }
}
