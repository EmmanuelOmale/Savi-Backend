namespace Savi.Core.Interfaces
{
    public interface IGroupWalletFundingServices
    {
        public Task<bool> AutoGroupSavings(string GroupId);

        public Task<bool> GroupAuto();

    }
}
