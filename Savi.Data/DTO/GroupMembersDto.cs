using Savi.Data.Enums;

namespace Savi.Data.DTO
{
    public class GroupMembersDto
    {
        public UserDTO User { get; set; }
        public string UserId { get; set; }

        public string GroupSavingsId { get; set; }

        public GroupSavingsRespnseDto GroupSavings { get; set; }

        public IsGroupOwner IsGroupOwner { get; set; }

        public int Positions { get; set; } = 1;

        public DateTime LastsavingsDate { get; set; }
    }
}
