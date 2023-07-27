using Savi.Data.Enums;

namespace Savi.Data.Domains
{
    public class SavingGoal 
    {
        public int Id { get; set; }
      
        public string GoalName { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal AmountToAddPerTime { get; set; }
        public string Frequency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public string WalletId { get; set; }
        public Wallet Wallet { get; set; }
		public decimal TotalContribution { get; set; }

	}
}
