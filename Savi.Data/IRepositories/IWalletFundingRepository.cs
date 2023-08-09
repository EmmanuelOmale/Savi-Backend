using Savi.Data.Domains;

namespace Savi.Data.IRepositories
{
	public interface IWalletFundingRepository
	{
		public Task<bool> CreateFundingWalletAsync(WalletFunding walletfunding);
	}
}