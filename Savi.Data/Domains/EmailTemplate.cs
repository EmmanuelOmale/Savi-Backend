using Savi.Data.Enums;

namespace Savi.Data.Domains
{
	public class EmailTemplate : BaseEntity
	{
		public string Subject { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
		public EmailPurpose Purpose { get; set; }
		public const string RegistrationLinkPlaceholder = "{registrationLink}";
	}
}