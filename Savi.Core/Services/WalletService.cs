using Microsoft.AspNetCore.Http;
using Savi.Core.Interfaces;
using Savi.Data.DTO;
using Savi.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var wallet =await _walletRepository.GetWalletByPhoneNumber(walletId);
            if (wallet == null)
            {
                return new APIResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = false,
                    Message = "User's wallet not found."

                };
            }

            if (wallet.Balance < amount)
            {
                return new APIResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest.ToString(),
                    IsSuccess = false,
                    Message = "Insufficient funds in the wallet."
                };
            }

            wallet.Balance -= amount;
           _walletRepository.UpdateWallet(wallet);
            return new APIResponse()
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                IsSuccess = true,
                Message = "Wallet debited successfully.",
                Result = wallet
                
            };

        }
    }
}
