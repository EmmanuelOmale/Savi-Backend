using Savi.Data.Enums;

namespace Savi.Data.Domains
{
	public class GroupSavingsFunding : BaseEntity
	{
		public string GroupSavingsId { get; set; }
		public GroupSavings GroupSavings { get; set; }
		public TransactionType TransactionType { get; set; }
		public decimal Amount { get; set; }

		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
	}
}