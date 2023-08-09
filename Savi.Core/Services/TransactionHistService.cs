using Microsoft.EntityFrameworkCore;
using Savi.Core.Interfaces;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Core.Services
{
	public class TransactionHistService : ITransactionHistService
	{
		private readonly SaviDbContext _dbContext;

		public TransactionHistService(SaviDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IQueryable<UserTransaction> GetOldestUserTransactions(string userId, int page)
		{
			var pageSize = 5;
			var skipCount = (page - 1) * pageSize;

			var transactions = GetTransactions(userId)
				.OrderBy(t => t.CreatedAt)
				.Skip(skipCount)
				.Take(pageSize)
				.Select(t => new UserTransaction
				{
					CreatedAt = t.CreatedAt,
					Amount = t.Amount,
					Description = t.Description,
				});

			return transactions;
		}
		public IQueryable<UserTransaction> GetRecentUserTransactions(string userId, int page)
		{
			var pageSize = 5;
			var skipCount = (page - 1) * pageSize;

			var lastWeek = DateTime.UtcNow.AddDays(-7);

			var transactions = GetTransactions(userId)
				.Where(t => t.CreatedAt >= lastWeek) 
				.OrderByDescending(t => t.CreatedAt)
				.Skip(skipCount)
				.Take(pageSize)
				.Select(t => new UserTransaction
				{
					CreatedAt = t.CreatedAt,
					Amount = t.Amount,
					Description = t.Description,
				});

			return transactions;
		}


		public IQueryable<UserTransaction> GetMoreUserTransactions(string userId, int page)
		{
			var pageSize = 5;
			var skipCount = (page - 1) * pageSize;

			var transactions = GetTransactions(userId)
				.OrderBy(t => t.CreatedAt)
				.Skip(skipCount)
				.Take(pageSize)
				.Select(t => new UserTransaction
				{
					CreatedAt = t.CreatedAt,
					Amount = t.Amount,
					Description = t.Description,
				});
			return transactions;
		}

		private IQueryable<UserTransaction> GetTransactions(string userId)
		{
			var walletTransactions = _dbContext.WalletFundings
				.Where(wf => wf.Wallet.UserId == userId)
				.Select(wf => new UserTransaction
				{
					TransactionType = wf.TransactionType.ToString(),
					Description = wf.Description,
					Amount = wf.Amount,
					UserId = userId,
					User = wf.Wallet.User,
					CreatedAt = wf.CreatedAt
				})
				.ToList();

			var groupSavingsTransactions = _dbContext.GroupSavingsFundings
				.Where(gs => gs.UserId == userId)
				.Select(gs => new UserTransaction
				{
					TransactionType = gs.TransactionType.ToString(),
					Description = gs.GroupSavings.PurPoseAndGoal,
					Amount = gs.Amount,
					UserId = userId,
					User = gs.User,
					CreatedAt = gs.CreatedAt
				})
				.ToList();

			var setTargetTransactions = _dbContext.SetTargetFundings
				 .Where(joined => joined.SetTarget.UserId == userId)
			
				.Select(joined => new UserTransaction
				{
					TransactionType = joined.TransactionType.ToString(),
					Description = joined.SetTarget.Target,
					Amount = joined.Amount,
					UserId = userId,
					User = joined.Wallet.User,
					CreatedAt = joined.CreatedAt
				})
				.ToList();

			var allTransactions = walletTransactions
				.Concat(groupSavingsTransactions)
				.Concat(setTargetTransactions)
				.AsQueryable();

			return allTransactions;
		}

	}
}
