using Hangfire;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Savi.Core.Interfaces;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.Enums;
using Savi.Data.IRepositories;
using Savi.Data.IRepository;
using Savi.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Core.Services
{
    public class SavingsService : ISavingsService
    {
        private readonly SaviDbContext _dbContext;
        private readonly IWalletService _walletService;
        private readonly ISavingGoalRepository _savingGoalRepository;


        public SavingsService(SaviDbContext dbContext, IWalletService walletService, ISavingGoalRepository savingGoalRepository)
        {
            _dbContext = dbContext;
            _walletService = walletService;
            _savingGoalRepository = savingGoalRepository;
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
                saving.TargetAmount += amount;

                await _savingGoalRepository.UpdateGoal(id, saving);
                var walletId = saving.WalletId;
                await _walletService.DebitWallet(walletId, amount);

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
