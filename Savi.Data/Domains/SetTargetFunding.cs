using Savi.Data.Enums;

namespace Savi.Data.Domains
{
	public class SetTargetFunding : BaseEntity
	{
		public decimal Amount { get; set; }
		public TransactionType TransactionType { get; set; }
		public SetTarget SetTarget { get; set; }
		public Guid SetTargetId { get; set; }

		public Wallet Wallet { get; set; }
		public string walletId { get; set; }
	}
}