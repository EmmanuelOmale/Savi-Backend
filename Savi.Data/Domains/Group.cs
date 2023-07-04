namespace Savi.Data.Domains
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int Schedule { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsActive { get; set; }
        public decimal ContributionAmount { get; set; }
        public bool IsOpen { get; set; }
        public int MaxNumberOfParticipants { get; set; }
        public DateTime CashoutDate { get; set; }
        public DateTime NextDueDate { get; set; }


        public ICollection<GroupTransaction> GroupTransactions { get; set; }
    }
}