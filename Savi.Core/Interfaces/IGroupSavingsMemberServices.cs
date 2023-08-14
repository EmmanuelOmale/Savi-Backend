using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
    public interface IGroupSavingsMemberServices
    {
        Task<ResponseDto<bool>> JoinGroupSavings(string UserId, string GroupId);
        Task<List<GroupMembersDto>> GetListOFGroupMember(string GroupId);
        Task<List<GroupMembersDto>> GetListOFGroupMemberByUserId(string UserId);


    }
}