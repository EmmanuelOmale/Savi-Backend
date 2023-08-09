using Savi.Data.Domains;

namespace Savi.Data.IRepositories
{
	public interface IGroupsavingsFundingRepository
	{
		public Task<bool> CreateGroupSavingsFundingAsync(GroupSavingsFunding groupSavingsFunding);
	}
}