using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Data.IRepositories
{
	public interface IGroupSavingsRepository
	{
		public Task<bool> CreateGroupSavings(GroupSavings groupSaving);

		public Task<ResponseDto<GroupSavingsRespnseDto>> GetGroupByIdAsync(string Id);

		public Task<ICollection<GroupSavingsRespnseDto>> GetListOfGroupSavingsAsync();
	}
}