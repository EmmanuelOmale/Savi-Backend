using Savi.Data.Enums;

namespace Savi.Data.DTO
{
	public class SetTargetFundingDTO
	{
		public decimal Amount { get; set; }
		public decimal CummulativeAmount { get; set; }
		public TransactionType TransactionType { get; set; }
		public Guid SetTargetId { get; set; }
	}
}