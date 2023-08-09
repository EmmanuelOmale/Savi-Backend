namespace Savi.Data.DTO
{
	public class FundTransferRequestDto
	{
		public string SourceWalletId { get; set; }
		public string DestinationWalletId { get; set; }
		public decimal Amount { get; set; }
	}
}