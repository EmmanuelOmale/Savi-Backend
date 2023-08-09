using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
	public interface IWalletDebitService
	{
		public Task<PayStackResponseDto> WithdrawUserFundAsync(decimal amount, string walletId);
	}
}