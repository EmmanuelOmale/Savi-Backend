using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
	public interface IWalletCreditService
	{
		public Task<PayStackResponseDto> CreditUserFundAsync(decimal amount, string walletId);
	}
}