using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
    public interface IGroupSavingsServices
    {
        public Task<PayStackResponseDto> CreateGroupSavings(GroupSavingsDto groupSavingsDto);



    }
}
