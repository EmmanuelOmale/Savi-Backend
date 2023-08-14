using Savi.Data.Domains;
using Savi.Data.Enums;

namespace Savi.Data.DTO
{
	public class SetTargetFundingDTO : BaseEntity
	{
		public decimal Amount { get; set; }
		public TransactionType TransactionType { get; set; }
		public SetTargetDTO SetTarget { get; set; }
		public string SetTargetId { get; set; }



	}
}