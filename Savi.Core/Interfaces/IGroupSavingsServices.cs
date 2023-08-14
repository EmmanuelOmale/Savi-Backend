using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
    public interface IGroupSavingsServices
    {
        public Task<PayStackResponseDto> CreateGroupSavings(GroupSavingsDto groupSavingsDto);

        public Task<ResponseDto<GroupSavingsRespnseDto>> GetUserByIDAsync(string UserId);
        public Task<ResponseDto<IEnumerable<GroupSavingsRespnseDto>>> GetListOfSavingsGroupAsync();

        public Task<GroupSavings> GetGroupByID(string GroupId);
        public Task<IEnumerable<GroupSavings>> GetListOfSavingsGroups();
        public Task<ResponseDto<GroupSavingsRespnseDto>> GetUserByUserIDAsync(string UserId);
        Task<ResponseDto<IEnumerable<GroupSavingsRespnseDto>>> GetListOfSavingsGroupByUserIdAsync(string UserId);








    }
}
