using Microsoft.AspNetCore.Mvc;
using Savi.Core.Interfaces;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public WalletController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
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
            var response = await _paymentService.WithdrawFundAsync(amount, walletId);

            if (response != null && response.Status == true)
            {

                return Ok(response);

            }

            return BadRequest(response);
        }
    }
}
