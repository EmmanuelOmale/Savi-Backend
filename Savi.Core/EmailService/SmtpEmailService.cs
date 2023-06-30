using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Savi.Core.Interfaces;
using Savi.Data.Domains;

namespace Savi.Data.EmailService
{
    public class SmtpEmailService : IEmailService
    {
        private readonly string _fromMail;
        private readonly string _host;
        private readonly int _port;
        private readonly string _password;

        public SmtpEmailService(EmailSettings mailSettings)
        {
            _fromMail = mailSettings.Username!;
            _host = mailSettings.Host!;
            _port = mailSettings.Port!;
            _password = mailSettings.Password!;
        }

        public async Task SendMail(MailRequest request)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_fromMail);
            email.To.Add(MailboxAddress.Parse(request.ToMail));
            email.Subject = request.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = request.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_host, _port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_fromMail, _password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
