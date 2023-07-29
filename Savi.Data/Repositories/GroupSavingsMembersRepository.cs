using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
    public class GroupSavingsMembersRepository : IGroupSavingsMembersRepository
    {
        private readonly SaviDbContext _saviDbContext;

        public GroupSavingsMembersRepository(SaviDbContext saviDbContext)
        {
            _saviDbContext = saviDbContext;
        }



        public async Task<bool> CreateSavingsGroupMembersAsync(GroupSavingsMembers groupSavingsMembers)
        {
            var newMember = await _saviDbContext.GroupSavingsMembers.AddAsync(groupSavingsMembers);
            var result = _saviDbContext.SaveChanges();
            if (result > 0)
            {
                return true;

            }
            return false;
        }

        public async Task<int> GetListOfGroupMembersAsync(string GroupId)
        {
            var list = await _saviDbContext.GroupSavingsMembers.Where(x => x.GroupSavingsId == GroupId).ToListAsync();
            if (list.Count > 0)
            {
                return list.Count;
            }
            return 0;
        }
        public async Task<List<GroupSavingsMembers>> GetListOfGroupMembersAsync2(string GroupId)
        {
            var list = await _saviDbContext.GroupSavingsMembers.Where(x => x.GroupSavingsId == GroupId).ToListAsync();
            if (list.Count > 0)
            {
                return list;
            }
            return null;
        }

        public async Task<int> GetUserLastUserPosition()
        {
            int highestPosition = _saviDbContext.GroupSavingsMembers.OrderByDescending(user => user.Positions)
                              .Select(user => user.Positions)
                              .FirstOrDefault();
            return highestPosition;
        }
        public async Task<List<int>> GetUserFirstUserPosition()
        {
            var highestPosition = await _saviDbContext.GroupSavingsMembers.OrderByDescending(user => user.Positions)
                               .Select(user => user.Positions)
                               .ToListAsync();
            return highestPosition;
        }





    }
}
