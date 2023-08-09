using Savi.Data.Domains;

namespace Savi.Data.IRepositories
{
	public interface IWalletRepository
	{
		public Task<bool> VerifyPaymentAsync(Wallet wallet);

		public Task<bool> CreateWalletAsync(Wallet wallet);

		public Task<Wallet> GetWalletByPhoneNumber(string PhoneNumber);

		public Task<decimal?> GetBalanceAsync(string Id);

		public Task<bool> DebitUser(Wallet wallet);

		public void UpdateWallet(Wallet wallet);

		Task<Wallet> GetUserWalletAsync(string userId);

		Task<bool> TransferFundsAsync(string sourceWalletId, string destinationWalletId, decimal amount);

		public List<UserTransaction> GetUserTransactions(string userId);
	}
}