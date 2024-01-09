
using System.Net;
using System.Net.Mail;

namespace rpglms_backend.src.shared
{
    public class EmailSender(string smtpServer, int smtpPort, string smtpUser, string smtpPass) : IEmailSender
    {
        private readonly string _smtpServer = smtpServer;
        private readonly int _smtpPort = smtpPort;
        private readonly string _smtpUser = smtpUser;
        private readonly string _smtpPass = smtpPass;

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true,
            };

            return client.SendMailAsync(_smtpUser, email, subject, htmlMessage);
        }
    }
}