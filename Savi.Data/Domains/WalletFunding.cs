using Savi.Data.Enums;

namespace Savi.Data.Domains
{
	public class WalletFunding : BaseEntity
	{
		public decimal Amount { get; set; }
		public decimal Cummulative { get; set; }
		public string Reference { get; set; }
		public string Narration { get; set; }
		public string TransactionCode { get; set; }
		public bool Status { get; set; }
		public TransactionType TransactionType { get; set; }
		public string Description { get; set; }

		public string WalletId { get; set; }
		public Wallet Wallet { get; set; }
	}
}