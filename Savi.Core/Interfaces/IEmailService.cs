﻿using Savi.Data.Domains;
using Savi.Data.Enums;

namespace Savi.Core.Interfaces
{
	public class MailRequest
	{
		public string ToMail { get; set; } = string.Empty;
		public string Subject { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
		public string Purpose { get; set; }
	}

	public interface IEmailService
	{
		public Task SendMail(UserAction userAction, string userEmail);

		Task AddEmailTemplate(EmailTemplate template);

		Task SendEmailAsync(string toEmail, string subject, string content);
	}
}