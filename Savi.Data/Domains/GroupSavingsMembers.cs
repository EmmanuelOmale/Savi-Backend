using Savi.Data.Enums;

namespace Savi.Data.Domains
{
    public class GroupSavingsMembers : BaseEntity
    {

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

		public string GroupSavingsId { get; set; }

		public GroupSavings GroupSavings { get; set; }

		public IsGroupOwner IsGroupOwner { get; set; }

        public int Positions { get; set; } = 1;

		public DateTime LastsavingsDate { get; set; }



    }
}
