using System.Diagnostics.CodeAnalysis;
using MimeKit;

namespace Template.Common.Helpers.MailKitHelper
{
    [ExcludeFromCodeCoverage]
    public class EmailMessage
    {
        public MailboxAddress Sender { get; set; }
        public MailboxAddress Receiver { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
