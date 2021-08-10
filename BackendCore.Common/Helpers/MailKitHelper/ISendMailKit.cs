namespace BackendCore.Common.Helpers.MailKitHelper
{
    public interface ISendMailKit
    {
        void Send(string mailTo, string body, string subject, bool supportHtml = false);
    }
}