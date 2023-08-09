using Savi.Data.Enums;

namespace Savi.Data.DTO
{
	public class SavingGoalsDTO
	{
		public string GoalName { get; set; }
		public decimal TargetAmount { get; set; }
		public decimal AmountToAddPerTime { get; set; }
		public string Frequency { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public TransactionType TransactionType { get; set; }
		public string WalletId { get; set; }
		public decimal TotalContribution { get; set; }
	}
}