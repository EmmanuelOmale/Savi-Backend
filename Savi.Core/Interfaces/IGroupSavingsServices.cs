using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
	public interface IGroupSavingsServices
	{
		public Task<PayStackResponseDto> CreateGroupSavings(GroupSavingsDto groupSavingsDto);

		public Task<ResponseDto<GroupSavingsRespnseDto>> GetUsrByIDAsync(string UserId);

		public Task<ResponseDto<IEnumerable<GroupSavingsRespnseDto>>> GetListOfSavingsGroupAsync();
	}
}