using Savi.Data.Domains;

namespace Savi.Data.IRepositories
{
	public interface IGroupSavingsMembersRepository
	{
		public Task<bool> CreateSavingsGroupMembersAsync(GroupSavingsMembers groupSavingsMembers);

		public Task<int> GetListOfGroupMembersAsync(string GroupId);

		public Task<List<GroupSavingsMembers>> GetListOfGroupMembersAsync2(string GroupId);

		public Task<List<int>> GetUserFirstUserPosition();

		public Task<int> GetUserLastUserPosition();
	}
}