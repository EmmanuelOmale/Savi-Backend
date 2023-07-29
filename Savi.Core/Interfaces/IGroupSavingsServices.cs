using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
    public interface IGroupSavingsServices
    {
        public Task<PayStackResponseDto> CreateGroupSavings(GroupSavingsDto groupSavingsDto);

        public Task<ResponseDto<GroupSavings>> GetUsrByIDAsync(string UserId);
        public Task<ResponseDto<IEnumerable<GroupSavings>>> GetListOfSavingsGroupAsync();



    }
}
