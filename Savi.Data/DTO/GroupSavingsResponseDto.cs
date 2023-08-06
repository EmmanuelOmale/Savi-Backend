using Savi.Data.Domains;
using Savi.Data.Enums;

namespace Savi.Data.DTO
{
    public class GroupSavingsRespnseDto : BaseEntity
    {
        public string UserId { get; set; }
        public string SavesName { get; set; }
        public UserDTO User { get; set; }
        public decimal ContributionAmount { get; set; }
        public GroupStatus GroupStatus { get; set; }

        public DateTime NextRunTime { get; set; }

        public int Count { get; set; }

        public DateTime ExpectedstartDate { get; set; }

        public DateTime ActualStartDate { get; set; }
        public int MemberCount { get; set; }

        public DateTime ActualEndDate { get; set; }

        public int FrequecncyId { get; set; }

        public int Runtime { get; set; }
        public string SavePortraitUrl { get; set; }
        public string SaveLandScape { get; set; }
        public string PurPoseAndGoal { get; set; }

        public string TermsAndCondition { get; set; }
    }
}
