using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
	public interface IGroupSavingsMemberServices
	{
		public Task<ResponseDto<bool>> JoinGroupSavings(string UserId, string GroupId);
	}
}