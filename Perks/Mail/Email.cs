using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perks.Mail
{
    /// <summary>
    /// Represents an email message that can be sent using any <see cref="IMailer"/> service.
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Gets or sets the from address for this email message.
        /// </summary>
        /// <value>From address for this email message.</value>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the address collection that contains the recipients of this email message.
        /// </summary>
        /// <value>The collection of email addresses.</value>
        public IEnumerable<string> To { get; set; }

        /// <summary>
        /// Gets or sets the subject line for this e-mail message.
        /// </summary>
        /// <value>The subject for the email message.</value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the message body.
        /// </summary>
        /// <value>The message body.</value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mail message body is in HTML. Default is <c>false</c>.
        /// </summary>
        /// <value><c>true</c> if the message body is in HTML; otherwise, <c>false</c>.</value>
        public bool IsHtml { get; set; }
    }
}
