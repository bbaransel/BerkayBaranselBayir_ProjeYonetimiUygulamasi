namespace Yonetimsell.UI.EmailServices.Abstract
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string emailTo, string subject, string htmlMessage);
    }
}
