using Savi.Data.Enums;

namespace Savi.Data.DTO
{
	public class SetTargetDTO
	{
		public string UserId { get; set; }
		public string TargetName { get; set; }
		public decimal TargetAmount { get; set; }
		public decimal AmountToSave { get; set; }
		public FrequencyType Frequency { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime WithdrawalDate { get; set; }
	}
}