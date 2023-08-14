using Savi.Data.Enums;

namespace Savi.Data.Domains
{
	public class SetTarget : BaseEntity
	{
		public string TargetName { get; set; }
		public decimal TargetAmount { get; set; }
		public decimal AmountToSave { get; set; }
		public FrequencyType Frequency { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public DateTime WithdrawalDate { get; set; }
		public ICollection<SetTargetFunding> SetTargetFunding { get; set; }
		public string SetTargetFundingId { get; set; }
		public decimal CumulativeAmount { get; set; }
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
		public int Runtime { get; set; }
		public DateTime NextRuntime { get; set; }

	}
}