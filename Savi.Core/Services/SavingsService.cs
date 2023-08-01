using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Savi.Core.Interfaces;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.Enums;
using Savi.Data.IRepositories;
using Savi.Data.IRepository;
using System.Security.Claims;

namespace Savi.Core.Services
{
	public class SavingsService : ISavingsService
    {
        private readonly SaviDbContext _dbContext;
        private readonly IWalletService _walletService;
		private readonly ISetTargetRepository _targetRepository;
        private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;



        public SavingsService(SaviDbContext dbContext, IWalletService walletService, IMapper mapper, ISetTargetRepository setTarget, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _walletService = walletService;
            _mapper = mapper;
			_targetRepository = setTarget;
			_userRepository = userRepository;
        }

		public async Task<APIResponse> FundTargetSavings(Guid id, decimal amount, string userId)
		{
			var savings = await _targetRepository.GetTargetById(id); 
			if (savings == null)
			{
				return new APIResponse()
				{
					IsSuccess = false,
					StatusCode = StatusCodes.Status404NotFound.ToString(),
					Message = "Savings Goal Not Found",
				};
			}
			var walletId = await _walletService.GetUserWalletAsync(userId);
			var debitResult = await _walletService.DebitWallet(walletId.Result.WalletId, amount);
			if (!debitResult.IsSuccess)
			{
				return new APIResponse()
				{
					IsSuccess = false,
					StatusCode = StatusCodes.Status400BadRequest.ToString(),
					Message = "Insufficient balance in the wallet.",
				};
			}

			savings.Result.CumulativeAmount += amount;

			var fundingDetails = new SetTargetFunding
			{
				Amount = amount,
				TransactionType = TransactionType.Funding, 
				SetTarget = savings.Result,
				SetTargetId = savings.Result.Id,
				walletId = walletId.Result.WalletId,
			};

			await _targetRepository.UpdateTarget(id, savings.Result);
			return new APIResponse()
			{
				IsSuccess = true,
				StatusCode = StatusCodes.Status200OK.ToString(),
				Message = "Saving Credited Successfully",
				Result = savings 
			};
		}

	}
}
