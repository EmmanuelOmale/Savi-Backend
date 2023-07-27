using AutoMapper;
using Microsoft.AspNetCore.Http;
using Savi.Core.Interfaces;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepository;


namespace Savi.Core.Services
{
	public class SavingsService : ISavingsService
    {
        private readonly SaviDbContext _dbContext;
        private readonly IWalletService _walletService;
        private readonly ISavingGoalRepository _savingGoalRepository;
        private readonly IMapper _mapper;


        public SavingsService(SaviDbContext dbContext, IWalletService walletService, ISavingGoalRepository savingGoalRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _walletService = walletService;
            _savingGoalRepository = savingGoalRepository;
            _mapper = mapper;
        }

        public async Task<APIResponse> FundTargetSavings(int id, decimal amount)
		{
			
			var savings = await _savingGoalRepository.GetGoalById(id);
           if(savings == null)
            {
                return new APIResponse()
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    Message = "Savings Goal Not Found",
                };
            }
                var saving = savings.Result;
            //var totalSavings = new Saving()
            //{
            //    GoalAmount = saving.TargetAmount,
            //    TotalContribution = 
            //};
			var walletId = saving.WalletId;
			var wallet = await _walletService.DebitWallet(walletId, amount);
			    saving.TargetAmount += amount;
			    var save = _mapper.Map<SavingGoal>(saving);
			    await _savingGoalRepository.UpdateGoal(id, save);

            return new APIResponse()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK.ToString(),
                Message = "saving Credited Successfully",
                Result = saving
            };

        }
       
    }
}
