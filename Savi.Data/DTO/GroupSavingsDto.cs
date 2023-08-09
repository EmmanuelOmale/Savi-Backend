namespace Savi.Data.DTO
{
	public class GroupSavingsDto
	{
		public string UserId { get; set; }
		public string SavesName { get; set; }
		public decimal ContributionAmount { get; set; }
		public DateTime ExpectedstartDate { get; set; }
		public DateTime ExpectedendDate { get; set; }

		public int SavingsFrequencyId { get; set; }

		public DateTime Runtime { get; set; }

		public string PurPoseAndGoal { get; set; }

		public string TermsAndCondition { get; set; }

		public string SavePortraitUrl { get; set; }

		public string SaveLandScape { get; set; }
	}
}