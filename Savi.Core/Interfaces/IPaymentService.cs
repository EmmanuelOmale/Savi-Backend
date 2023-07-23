using Savi.Data.DTO;

namespace Savi.Core.Interfaces
{
    public interface IPaymentService
    {
        public Task<PayStackResponseDto> VerifyPaymentAsync(string reference);
        public Task<PayStackResponseDto> WithdrawFundAsync(decimal amount, string walletId);



    }
}
