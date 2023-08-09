namespace Savi.Data.DTO
{
	public class TransactionDTO
	{
		public string TransactionType { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
		public string Reference { get; set; }
	}
}