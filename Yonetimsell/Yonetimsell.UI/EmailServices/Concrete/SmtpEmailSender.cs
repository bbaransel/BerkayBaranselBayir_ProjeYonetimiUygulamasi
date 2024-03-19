using System.Net;
using System.Net.Mail;
using Yonetimsell.UI.EmailServices.Abstract;

namespace Yonetimsell.UI.EmailServices.Concrete
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _enableSsl;
        private readonly string _username;
        private readonly string _password;

        public SmtpEmailSender(string host, int port, bool enableSsl, string username, string password)
        {
            _host = host;
            _port = port;
            _enableSsl = enableSsl;
            _username = username;
            _password = password;
        }

        public async Task SendEmailAsync(string emailTo, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = _enableSsl
            };
            await client.SendMailAsync(new MailMessage(_username, emailTo, subject, htmlMessage)
            {
                IsBodyHtml = true
            });
        }
    }
}
