namespace Savi.Data.DTO
{
    public class GroupSavingsRespnseDto
    {
        public string UserId { get; set; }
        public string SavesName { get; set; }
        public UserDTO User { get; set; }
        public decimal ContributionAmount { get; set; }

        public DateTime ExpectedstartDate { get; set; }

        public DateTime ActualStartDate { get; set; }
        public int MemberCount { get; set; }

        public DateTime ActualEndDate { get; set; }

        public int FrequecncyId { get; set; }

        public DateTime Runtime { get; set; }
        public string SavePortraitUrl { get; set; }
        public string SaveLandScape { get; set; }
    }
}
