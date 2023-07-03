using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using Savi.Core.Interfaces;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.Enums;
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
        private readonly ILogger<SmtpEmailService> _logger;

        public SmtpEmailService(EmailSettings mailSettings, ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, ILogger<SmtpEmailService> logger)
        {
            _fromMail = mailSettings.Username!;
            _host = mailSettings.Host!;
            _port = mailSettings.Port!;
            _password = mailSettings.Password!;
            _dbContext = dbContext;
            _httpContextAccessor = contextAccessor;
            _logger = logger;
        }

        public async Task SendMail(UserAction userAction)
        {
            try
            {
                var userEmail = _httpContextAccessor.HttpContext!.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                var purpose = GetPurposeFromUserAction(userAction);
                var template = await GetEmailTemplateByPurpose(purpose);

                var request = new MailRequest
                {
                    ToMail = userEmail!,
                    Subject = template.Subject,
                    Body = template.Body,
                    Purpose = template.Purpose,
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

                _logger.LogInformation($"Email sent to {request.ToMail} with purpose {request.Purpose}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending email");
                throw;
            }
        }
        private async Task<EmailTemplate> GetEmailTemplateByPurpose(EmailPurpose purpose)
        {
            var template = await _dbContext.EmailTemplates.FirstOrDefaultAsync(t => t.Purpose == purpose);

            return template!;
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

        private EmailPurpose GetPurposeFromUserAction(UserAction userAction)
        {
            switch (userAction)
            {
                case UserAction.Registration:
                    return EmailPurpose.Registration;
                case UserAction.PasswordReset:
                    return EmailPurpose.PasswordReset;
                case UserAction.Newsletter:
                    return EmailPurpose.Newsletter;
                default:
                    throw new InvalidOperationException("Unable to determine the email purpose.");
            }
        }
    }
}
