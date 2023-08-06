using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
    public class GroupSavingFundingRepository : IGroupsavingsFundingRepository
    {
        private readonly SaviDbContext _saviDbContext;

        public GroupSavingFundingRepository(SaviDbContext saviDbContext)
        {
            _saviDbContext = saviDbContext;
        }
        public async Task<bool> CreateGroupSavingsFundingAsync(GroupSavingsFunding groupSavingsFunding)
        {

            var entry = await _saviDbContext.GroupSavingsFundings.AddAsync(groupSavingsFunding);
            int rowsAffected = await _saviDbContext.SaveChangesAsync();

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}
