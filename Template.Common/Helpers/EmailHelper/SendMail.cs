using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Template.Common.Helpers.EmailHelper
{
    [ExcludeFromCodeCoverage]
    public class SendMail : ISendMail
    {
        private readonly IConfiguration _configuration;
        public SendMail(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(string mailTo, string body, string subject, bool supportHtml = false)
        {
            try
            {
                SmtpClient client = new SmtpClient(_configuration["Email:Smtp:Host"], int.Parse(_configuration["Email:Smtp:Port"]));
                var value = _configuration["Email:Smtp:EnableSSL"];
                var enableSsl = bool.Parse(value);
                client.UseDefaultCredentials = false;
                client.EnableSsl = enableSsl;
                var username = _configuration["Email:Smtp:Username"];
                var password = _configuration["Email:Smtp:Password"];
                client.Credentials = new NetworkCredential(username, password);
                //var url = $"{_configuration["FrontendUrl"]}reset?token={body}";
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(mailTo);
                mailMessage.To.Add(mailTo);
                mailMessage.Body = body;
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = supportHtml;
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}