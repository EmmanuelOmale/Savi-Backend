﻿using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Data.IRepositories
{
	public interface IGroupSavingsMembersRepository
	{
		public Task<bool> CreateSavingsGroupMembersAsync(GroupSavingsMembers groupSavingsMembers);

		public Task<int> GetListOfGroupMembersAsync(string GroupId);

		public Task<List<GroupSavingsMembers>> GetListOfGroupMembersAsync2(string GroupId);
		public Task<List<GroupMembersDto>> GetListOfGroupMembersAsync3(string GroupId);


		public Task<List<int>> GetUserFirstUserPosition();

		public Task<int> GetUserLastUserPosition();
		public Task<int> GetUserLastUserPosition2(string GrouId);

		Task<bool> Check_If_UserExist(string UserId, string GroupId);
		Task<List<GroupMembersDto>> GetListOfGroupMembersByUserId(string UserId);




	}
}