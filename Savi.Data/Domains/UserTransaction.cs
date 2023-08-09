namespace Savi.Data.Domains
{
	public class UserTransaction : BaseEntity
	{
		public string TransactionType { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
		public string Reference { get; set; }
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
	}
}