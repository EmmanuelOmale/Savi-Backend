using Savi.Data.Domains;

namespace Savi.Core.Interfaces
{
    public interface IGroupFund
    {
        public Task<bool> GroupFundingMember(GroupSavingsFunding groupSavings);

    }
}
