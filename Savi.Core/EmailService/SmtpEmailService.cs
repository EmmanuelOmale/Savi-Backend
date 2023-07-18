using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using Savi.Core.Interfaces;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.Enums;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Savi.Data.EmailService
{
    public class SmtpEmailService : IEmailService
    {
        private readonly string _fromMail;
        private readonly string _host;
        private readonly int _port;
        private readonly string _password;
        private readonly SaviDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<SmtpEmailService> _logger;
        private readonly IConfiguration _config;
        public SmtpEmailService(EmailSettings mailSettings, SaviDbContext dbContext, IHttpContextAccessor contextAccessor, ILogger<SmtpEmailService> logger, IConfiguration config)
        {
            _fromMail = mailSettings.Username!;
            _host = mailSettings.Host!;
            _port = mailSettings.Port!;
            _password = mailSettings.Password!;
            _dbContext = dbContext;
            _httpContextAccessor = contextAccessor;
            _logger = logger;
            _config = config;
        }

        public async Task SendMail(UserAction userAction, string userEmail)
        {
            try
            {


                var purpose = GetPurposeFromUserAction(userAction);
                var template = await GetEmailTemplateByPurpose(purpose.ToString());
                string registrationLink = GenerateRegistrationLink(userAction);

                string emailBody = template.Body.Replace(template.Body, registrationLink);


                var request = new MailRequest
                {
                    ToMail = userEmail!,
                    Subject = template.Subject,
                    Purpose = template.Purpose.ToString(),
                };
                if (userAction == UserAction.Registration || userAction == UserAction.PasswordReset)
                {

                    request.Body = GenerateEmailBody(emailBody, userAction);
                }
                else
                {
                    request.Body = template.Body;
                }

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_fromMail);
                email.To.Add(MailboxAddress.Parse(request.ToMail));
                email.Subject = request.Subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_host, _port, SecureSocketOptions.SslOnConnect);
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
        private async Task<EmailTemplate> GetEmailTemplateByPurpose(string purpose)
        {
            var template = await _dbContext.EmailTemplates
                        .ToListAsync();

            var filteredTemplate = template.FirstOrDefault(t => t.Purpose.ToString() == purpose.ToString());
            return filteredTemplate!;

        }

        public async Task AddEmailTemplate(EmailTemplate template)
        {


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

        private string GenerateEmailBody(string body, UserAction userAction)
        {
            string registrationLink = GenerateRegistrationLink(userAction);

            string emailBody = body.Replace("{registrationLink}", registrationLink);

            return emailBody;
        }

        private string GenerateRegistrationLink(UserAction userAction)
        {

            string userId = GetUserIdFromHttpContext();
            string token = GenerateUniqueToken();

            string registrationLink = $"https://example.com/register?userId={userId}&token={token}";

            return registrationLink;
        }
        private string GetUserIdFromHttpContext()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
        private string GenerateUniqueToken()
        {
            const int tokenLength = 10;
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var random = new Random();
            var token = new StringBuilder();

            for (int i = 0; i < tokenLength; i++)
            {
                int randomIndex = random.Next(0, allowedChars.Length);
                char randomChar = allowedChars[randomIndex];
                token.Append(randomChar);
            }

            return token.ToString();
        }

        public async Task SendPassWordResetEmailAsync(string toEmail, string subject, string content)
        {
            var emailSettings = _config.GetSection("EmailSettings");

            var from = new MailAddress(emailSettings["Username"], "Savi");
            var to = new MailAddress(toEmail);

            using (var message = new MailMessage(from, to))
            {
                message.Subject = subject;
                message.Body = content;
                message.IsBodyHtml = true;

                using (var client = new System.Net.Mail.SmtpClient(emailSettings["Host"], int.Parse(emailSettings["Port"])))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["Password"]);

                    await client.SendMailAsync(message);
                }
            }
        }



    }
}
