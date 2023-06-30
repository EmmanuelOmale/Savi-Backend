using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Task SendMail(MailRequest request);
    }
}
