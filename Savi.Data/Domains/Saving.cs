namespace Savi.Data.Domains
{
    public class Saving : BaseEntity
    {

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal TotalContribution { get; set; }
        public string Purpose { get; set; }
        public string Avatar { get; set; }
        public DateTime TargetDate { get; set; }
    }
}