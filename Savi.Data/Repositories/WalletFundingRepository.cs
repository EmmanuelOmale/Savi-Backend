using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
    public class WalletFundingRepository : IWalletFundingRepository
    {
        private readonly SaviDbContext _saviDbContext;

        public WalletFundingRepository(SaviDbContext saviDbContext)
        {
            _saviDbContext = saviDbContext;
        }



        public async Task<bool> CreateFundingWalletAsync(WalletFunding walletfunding)
        {

            var entry = await _saviDbContext.WalletFundings.AddAsync(walletfunding);
            int rowsAffected = await _saviDbContext.SaveChangesAsync();

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

    }
}
