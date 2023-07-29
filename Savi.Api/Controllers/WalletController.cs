using Microsoft.AspNetCore.Mvc;
using Savi.Core.Interfaces;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IWalletCreditService _walletCreditService;
        private readonly IGroupSavingsServices _groupSavingsServices;
        private readonly IWalletDebitService _walletDebitService;

        public WalletController(IPaymentService paymentService, IWalletCreditService walletCreditService,
            IGroupSavingsServices groupSavingsServices,
            IWalletDebitService walletDebitService)
        {
            _paymentService = paymentService;
            _walletCreditService = walletCreditService;
            _groupSavingsServices = groupSavingsServices;
            _walletDebitService = walletDebitService;
        }

        [HttpGet("/payment/verify/{reference}")]
        public async Task<IActionResult> VerifyPayment(string reference)
        {
            var response = await _paymentService.VerifyPaymentAsync(reference);

            if (response != null && response.Status == true)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("/payment/withdrawfund/{amount}/{walletId}")]
        public async Task<IActionResult> WithDrawFund(decimal amount, string walletId)
        {
            var response = await _walletDebitService.WithdrawUserFundAsync(amount, walletId);

            if (response != null && response.Status == true)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("/payment/creditfund/{amount}/{walletId}")]
        public async Task<IActionResult> CreditFund(decimal amount, string walletId)
        {
            var response = await _walletCreditService.CreditUserFundAsync(amount, walletId);

            if (response != null && response.Status == true)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}
