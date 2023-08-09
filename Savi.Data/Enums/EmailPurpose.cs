using System.ComponentModel;

namespace Savi.Data.Enums
{
	public enum EmailPurpose
	{
		[Description("Registration")]
		Registration,

		[Description("PasswordReset")]
		PasswordReset,

		[Description("Newsletter")]
		Newsletter
	}
}