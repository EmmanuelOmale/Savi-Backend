using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly SaviDbContext _SaviDb;
        public WalletRepository(SaviDbContext db)
        {
            _SaviDb = db;
        }
        public async Task<bool> VerifyPaymentAsync(Wallet wallet)
        {
            var newentry = _SaviDb.Wallets.Update(wallet);
            var rowsAffected = await _SaviDb.SaveChangesAsync();
            if(rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DebitUser(Wallet wallet)
        {
            var newentry = _SaviDb.Wallets.Update(wallet);
            var rowsAffected = await _SaviDb.SaveChangesAsync();
            if(rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> CreateWalletAsync(Wallet wallet)
        {
            var entry = await _SaviDb.Wallets.AddAsync(wallet);
            int rowsAffected = await _SaviDb.SaveChangesAsync();

            if(rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<Wallet> GetWalletByPhoneNumber(string PhoneNumber)
        {
            var entry = await _SaviDb.Wallets.FirstOrDefaultAsync(x => x.WalletId == PhoneNumber);
            if(entry != null)
            {
                return entry;
            }
            return null;
        }
        public async Task<decimal?> GetBalanceAsync(string Id)
        {
            var entry = await _SaviDb.Wallets.FirstOrDefaultAsync(x => x.WalletId == Id);
            if(entry != null)
            {
                var wallet = new Wallet()
                {
                    Balance = entry.Balance
                };
                return wallet.Balance;
            }
            return null;
        }
        public async Task<bool> UpdateWallet(Wallet wallet)
        {
            var updatewallet = _SaviDb.Wallets.Update(wallet);
            var entry = await _SaviDb.SaveChangesAsync();
            if(entry > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<Wallet> GetUserWalletAsync(string userId)
        {
            var wallet = await _SaviDb.Wallets.FirstOrDefaultAsync(x => x.UserId == userId);

            return wallet; // This might be null if the wallet does not exist, and that's okay.
        }
        public async Task<bool> TransferFundsAsync(string sourceWalletId, string destinationWalletId, decimal amount)
        {
            var sourceWallet = await _SaviDb.Wallets.FirstOrDefaultAsync(w => w.WalletId == sourceWalletId);
            var destinationWallet = await _SaviDb.Wallets.FirstOrDefaultAsync(w => w.WalletId == destinationWalletId);

            if(sourceWallet == null || destinationWallet == null || amount <= 0)
            {
                return false;
            }
            if(sourceWallet.Balance < amount)
            {
                return false;
            }
            sourceWallet.Balance -= amount;
            destinationWallet.Balance += amount;
            await _SaviDb.SaveChangesAsync();
            var transaction = new UserTransaction
            {
                TransactionType = "Transfer",
                Description = $"Funds transfer from {sourceWalletId} to {destinationWalletId}",
                Amount = amount,
                Reference = Guid.NewGuid().ToString(),
                UserId = sourceWallet.UserId,
            };
            _SaviDb.UserTransactions.Add(transaction);
            await _SaviDb.SaveChangesAsync();
            return true;
        }
        public List<UserTransaction> GetUserTransactions(string userId)
        {
            return _SaviDb.UserTransactions
                .Where(ut => ut.UserId == userId)
                .ToList();
        }
    }
}