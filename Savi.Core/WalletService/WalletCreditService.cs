using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Core.WalletService
{
	public class WalletCreditService : IWalletCreditService
	{
		private readonly IWalletFundingRepository _walletFunding;
		private readonly IWalletRepository _walletRepository;

		public WalletCreditService(IWalletFundingRepository walletFundingRepository, IWalletRepository walletRepository)
		{
			_walletFunding = walletFundingRepository;
			_walletRepository = walletRepository;
		}

		public async Task<PayStackResponseDto> CreditUserFundAsync(decimal amount, string walletId)
		{
			try
			{
				var UserWallet = await _walletRepository.GetWalletByPhoneNumber(walletId);
				if (UserWallet != null)
				{
					var walletbalance = UserWallet.Balance;
					var newbalance = walletbalance + amount;
					var walletupdate = await _walletRepository.VerifyPaymentAsync(UserWallet);
					var newWalletfunding = new WalletFunding()
					{
						WalletId = walletId,
						Amount = amount,
						TransactionType = Data.Enums.TransactionType.Withdrawal,
						Description = "Credit",
						Cummulative = newbalance,
					};
					var createnewWalletFund = await _walletFunding.CreateFundingWalletAsync(newWalletfunding);
					UserWallet.Balance = newbalance;
					UserWallet.WalletFundingId = newWalletfunding.Id;
					_walletRepository.UpdateWallet(UserWallet);
					var result31 = new PayStackResponseDto()
					{
						Status = true,
						Message = ($"You have successfully Credited {amount}, your new balance is {newbalance}"),
						Data = null
					};
					return result31;
				}
				var result2 = new PayStackResponseDto()
				{
					Status = false,
					Message = ("Wallet does not exist"),
					Data = null
				};
				return result2;
			}
			catch (Exception ex)
			{
				var result3 = new PayStackResponseDto()
				{
					Status = false,
					Message = ex.Message,
					Data = null
				};
				return result3;
			}
		}
	}
}