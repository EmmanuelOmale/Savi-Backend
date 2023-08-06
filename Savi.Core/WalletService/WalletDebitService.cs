using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Core.WalletService
{
    public class WalletDebitService : IWalletDebitService
    {
        private readonly IWalletFundingRepository _walletFunding;
        private readonly IWalletRepository _walletRepository;

        public WalletDebitService(IWalletFundingRepository walletFundingRepository, IWalletRepository walletRepository)
        {
            _walletFunding = walletFundingRepository;
            _walletRepository = walletRepository;
        }


        public async Task<PayStackResponseDto> WithdrawUserFundAsync(decimal amount, string walletId)
        {
            try
            {
                var UserWallet = await _walletRepository.GetWalletByPhoneNumber(walletId);
                if (UserWallet != null)
                {
                    decimal newbalance = 0;
                    var walletbalance = UserWallet.Balance;
                    if (amount <= walletbalance && amount > 0)
                    {
                        newbalance = walletbalance - amount;
                        UserWallet.Balance = newbalance;

                        var walletupdate = await _walletRepository.DebitUser(UserWallet);
                        var newWalletfunding = new WalletFunding()
                        {
                            WalletId = walletId,
                            Amount = amount,
                            TransactionType = Data.Enums.TransactionType.Funding,
                            Description = "Debit",
                            Cummulative = newbalance
                        };
                        var createnewWalletFund = await _walletFunding.CreateFundingWalletAsync(newWalletfunding);
                        var result31 = new PayStackResponseDto()
                        {
                            Status = true,
                            Message = ($"You have successfully withdraw {amount}, you have {newbalance} left"),
                            Data = null
                        };
                        return result31;
                    }
                    var result2 = new PayStackResponseDto()
                    {
                        Status = false,
                        Message = ("Insufficient balance"),
                        Data = null
                    };
                    return result2;
                };
                var result3 = new PayStackResponseDto()
                {
                    Status = false,
                    Message = ("Invalid Credentials"),
                    Data = null
                };
                return result3;



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
