namespace Savi.Data.Domains
{
    public class GroupSavings : BaseEntity
    {
        public IEnumerable<ApplicationUser> User { get; set; }
        public string UserId { get; set; }
        public string SavesName { get; set; }
        public decimal ContributionAmount { get; set; }

        public DateTime ExpectedstartDate { get; set; }

        public DateTime ActualStartDate { get; set; }

        public DateTime ActualEndDate { get; set; }

        public string FrequecncyNameId { get; set; }
        public int MemberCount { get; set; } = 5;

        public Frequency FrequencyName { get; set; }
        public DateTime Runtime { get; set; }


        public string PurPoseAndGoal { get; set; }

        public string TermsAndCondition { get; set; }

        public string GroupStatus { get; set; }

        public string SavePortraitUrl { get; set; }
        public string SaveLandScape { get; set; }

        public DateTime NextRunTime { get; set; }
    }







}
