using System;
using Microsoft.Extensions.Logging;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace BackendCore.Common.Helpers.MailKitHelper
{
    public class SendMailKit : ISendMailKit
    {
        private readonly EmailMetadata _emailMetadata;
        private readonly ILogger<SendMailKit> _logger;
        public SendMailKit(EmailMetadata emailMetadata, ILogger<SendMailKit> logger)
        {
            _emailMetadata = emailMetadata;
            _logger = logger;
        }
        //192.168.14.19
        public void Send(string mailTo, string body, string subject, bool supportHtml = false)
        {
            try
            {
                var message = new EmailMessage
                {
                    Sender = new MailboxAddress(_emailMetadata.SenderName, _emailMetadata.Sender),
                    Receiver = new MailboxAddress(mailTo , mailTo),
                    Subject = subject,
                    Content = body
                };
                var mimeMessage = CreateEmailMessage(message, supportHtml);
                var enableSsl = bool.Parse(_emailMetadata.EnableSsl);
                using var smtpClient = new SmtpClient();
                smtpClient.Connect(_emailMetadata.SmtpServer, _emailMetadata.Port, enableSsl);
                smtpClient.Authenticate(_emailMetadata.UserName, _emailMetadata.Password);
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception Message At Send Mail :  {e.Message}");
            }

        }
        private MimeMessage CreateEmailMessage(EmailMessage message, bool supportHtml)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Receiver);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(supportHtml ? MimeKit.Text.TextFormat.Html : MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return mimeMessage;
        }
    }
}
