using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.IRepositories;

namespace Savi.Core.GroupWalletFunding
{
    public class GroupFunding : IGroupFund
    {
        private readonly IGroupsavingsFundingRepository _groupsavingsFundingRepository;

        public GroupFunding(IGroupsavingsFundingRepository groupsavingsFundingRepository)
        {
            _groupsavingsFundingRepository = groupsavingsFundingRepository;
        }
        public async Task<bool> GroupFundingMember(GroupSavingsFunding groupSavings)
        {
            var groupfund = await _groupsavingsFundingRepository.CreateGroupSavingsFundingAsync(groupSavings);
            if(groupfund)
            {
                return true;
            }
            return false;
        }
    }
}
