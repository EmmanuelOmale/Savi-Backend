using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
	public interface IWalletService
	{
		Task<APIResponse> DebitWallet(string walletId, decimal amount);

		Task<ResponseDto<WalletDTO>> GetUserWalletAsync(string userId);

		Task<bool> TransferFundsAsync(string sourceWalletId, string destinationWalletId, decimal amount);

		List<TransactionDTO> GetUserTransactions(string userId);
	}
}