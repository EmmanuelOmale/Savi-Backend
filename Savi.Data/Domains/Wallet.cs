namespace Savi.Data.Domains
{
	public class Wallet : BaseEntity
	{
		public string WalletId { get; set; }
		public string Currency { get; set; }
		public decimal Balance { get; set; }
		public string Reference { get; set; }
		public string Pin { get; set; }
		public string Code { get; set; }
		public string PaystackCustomerCode { get; set; }

		public string SetWalletId(string phoneNumber)
		{
			if (phoneNumber.StartsWith("+234"))
			{
				phoneNumber = phoneNumber.Substring(4); // Remove '+234'
				return phoneNumber;
			}
			else if (phoneNumber.StartsWith("0"))
			{
				phoneNumber = phoneNumber.Substring(1); // Remove leading '0'
				return phoneNumber;
			}

			if (phoneNumber.Length == 10 && long.TryParse(phoneNumber, out long walletId))
			{
				WalletId = walletId.ToString();
				return phoneNumber;
			}
			else
			{
				throw new Exception("Invalid Phone Number Format");
			}
		}

		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
		public string WalletFundingId { get; set; }
		public ICollection<WalletFunding> WalletFunding { get; set; }
	}
}