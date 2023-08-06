using Microsoft.AspNetCore.Http;
using Savi.Core.Interfaces;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<APIResponse> DebitWallet(string walletId, decimal amount)
        {
            var wallet = await _walletRepository.GetWalletByPhoneNumber(walletId);
            if(wallet == null)
            {
                return new APIResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = false,
                    Message = "User's wallet not found."

                };
            }

            if(wallet.Balance < amount)
            {
                return new APIResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest.ToString(),
                    IsSuccess = false,
                    Message = "Insufficient funds in the wallet."
                };
            }

            wallet.Balance -= amount;
            await _walletRepository.UpdateWallet(wallet);
            return new APIResponse()
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                IsSuccess = true,
                Message = "Wallet debited successfully.",
                Result = wallet

            };


        }


        public async Task<ResponseDto<WalletDTO>> GetUserWalletAsync(string userId)
        {
            var wallet = await _walletRepository.GetUserWalletAsync(userId);

            if(wallet == null)
            {
                return new ResponseDto<WalletDTO>()
                {
                    StatusCode = 404,
                    // IsSuccess = false,
                    DisplayMessage = "User does not have a wallet.",
                };
            }

            // Convert the wallet to WalletDTO
            var walletDto = new WalletDTO
            {
                WalletId = wallet.WalletId,
                Currency = wallet.Currency,
                Balance = wallet.Balance,
                Reference = wallet.Reference,
                Pin = wallet.Pin,
                Code = wallet.Code,
                PaystackCustomerCode = wallet.PaystackCustomerCode,
            };

            return new ResponseDto<WalletDTO>()
            {
                StatusCode = 200,
                // IsSuccess = true,
                DisplayMessage = "User's wallet retrieved successfully.",
                Result = walletDto,
            };
        }

        public async Task<bool> TransferFundsAsync(string sourceWalletId, string destinationWalletId, decimal amount)
        {
            try
            {
                var transferSuccess = await _walletRepository.TransferFundsAsync(sourceWalletId, destinationWalletId, amount);
                return transferSuccess;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public List<TransactionDTO> GetUserTransactions(string userId)
        {
            var userTransactions = _walletRepository.GetUserTransactions(userId);
            return userTransactions.Select(ut => new TransactionDTO
            {
                TransactionType = ut.TransactionType,
                Description = ut.Description,
                Amount = ut.Amount,
                Reference = ut.Reference
            }).ToList();
        }



    }
}
