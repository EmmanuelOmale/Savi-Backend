
using Savi.Data.Domains;

namespace Savi.Core.Interfaces
{
    public class MailRequest 
    {
        public string ToMail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;


     
    }
    public interface IEmailService
    {
        public Task SendMail(string templateId);
        Task AddEmailTemplate(string subject, string body);
    }
}
