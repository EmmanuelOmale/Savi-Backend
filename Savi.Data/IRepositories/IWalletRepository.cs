using Savi.Data.Domains;
using Savi.Data.DTO;

namespace Savi.Data.IRepositories
{
    public interface IWalletRepository
    {
        public Task<bool> VerifyPaymentAsync(Wallet wallet);
        public Task<bool> CreateWalletAsync(Wallet wallet);
        public Task<Wallet> GetWalletByPhoneNumber(string PhoneNumber);
        public Task<decimal?> GetBalanceAsync(string Id);
        public void UpdateWallet(Wallet wallet);



    }
}
