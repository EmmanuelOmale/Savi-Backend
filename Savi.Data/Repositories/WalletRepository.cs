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
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CreateWalletAsync(Wallet wallet)
        {

            var entry = await _SaviDb.Wallets.AddAsync(wallet);
            int rowsAffected = await _SaviDb.SaveChangesAsync();

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<Wallet> GetWalletByPhoneNumber(string PhoneNumber)
        {
            var entry = _SaviDb.Wallets.FirstOrDefault(x => x.WalletId == PhoneNumber);
            if (entry != null)
            {
                return entry;
            }
            return null;
        }
        public async Task<decimal?> GetBalanceAsync(string Id)
        {
            var entry = _SaviDb.Wallets.FirstOrDefault(x => x.WalletId == Id);
            if (entry != null)
            {
                var wallet = new Wallet()
                {
                    Balance = entry.Balance
                };
                return wallet.Balance;
            }
            return null;
        }

    }
}
