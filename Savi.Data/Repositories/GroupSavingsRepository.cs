using Microsoft.AspNetCore.Http;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
    public class GroupSavingsRepository : IGroupSavingsRepository
    {
        private readonly SaviDbContext _saviDbContext;

        public GroupSavingsRepository(SaviDbContext saviDbContext)
        {
            _saviDbContext = saviDbContext;
        }



        public async Task<bool> CreateGroupSavings(GroupSavings groupSaving)
        {

            var newGroupsavings = await _saviDbContext.GroupSavings.AddAsync(groupSaving);
            var mekon = await _saviDbContext.SaveChangesAsync();
            if (mekon > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<ResponseDto<GroupSavings>> GetGroupByIdAsync(string Id)
        {
            var group = await _saviDbContext.GroupSavings.FindAsync(Id);
            if (group == null)
            {
                var notFoundResponse = new ResponseDto<GroupSavings>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    DisplayMessage = "User not found"
                };
                return notFoundResponse;
            }
            var success = new ResponseDto<GroupSavings>
            {
                StatusCode = StatusCodes.Status200OK,
                DisplayMessage = "Group Exist",
                Result = group
            };
            return success;

        }
        public async Task<ICollection<GroupSavings>> GetListOfGroupSavingsAsync()
        {
            var list = _saviDbContext.GroupSavings.ToList();
            if (list.Count > 0)
            {
                return list;
            }
            return null;
        }

    }
}
