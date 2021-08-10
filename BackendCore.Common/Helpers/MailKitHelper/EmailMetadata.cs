namespace Cex.Common.EmailHelper
{
    public class EmailMetadata
    {
        public string Sender { get; set; }
        public string SenderName { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EnableSsl { get; set; }
    }
}
