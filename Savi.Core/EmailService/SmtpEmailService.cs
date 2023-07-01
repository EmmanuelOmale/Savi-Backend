using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Savi.Core.Interfaces;
using Savi.Data.Context;
using Savi.Data.Domains;
using System.Security.Claims;

namespace Savi.Data.EmailService
{
    public class SmtpEmailService : IEmailService
    {
        private readonly string _fromMail;
        private readonly string _host;
        private readonly int _port;
        private readonly string _password;
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SmtpEmailService(EmailSettings mailSettings, ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            _fromMail = mailSettings.Username!;
            _host = mailSettings.Host!;
            _port = mailSettings.Port!;
            _password = mailSettings.Password!;
            _dbContext = dbContext;
            _httpContextAccessor = contextAccessor;
        }

        public async Task SendMail(string templateId)
        {
            var userEmail = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var template = await GetEmailTemplateById(templateId);

            var request = new MailRequest
            {
                ToMail = userEmail!,
                Subject = template.Subject,
                Body = template.Body,
            };

           

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

        private async Task<EmailTemplate> GetEmailTemplateById(string templateId)
        {
            var tempId = await _dbContext.EmailTemplates.FindAsync(templateId);

            return tempId!;
        }

        public async Task AddEmailTemplate(string subject, string body)
        {
            var template = new EmailTemplate
            {
                Subject = subject,
                Body = body,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.EmailTemplates.Add(template);
            await _dbContext.SaveChangesAsync();
        }
    }
}
