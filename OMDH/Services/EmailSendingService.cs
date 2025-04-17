using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using OMDH.Models;

namespace OMDH.Services
{
    public class EmailSendingService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailSendingService(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Config { get; }

        public void SendEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
