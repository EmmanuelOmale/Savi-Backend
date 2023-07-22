using Microsoft.AspNetCore.Mvc;
using Savi.Core.Interfaces;
using Savi.Data.DTO;

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
        [HttpPost("/payment/withdrawfund/{amount}")]
        public async Task<IActionResult> WithDrawFund(WalletFundingDto walletFunding)
        {
            var response = await _paymentService.WithdrawFundAsync(walletFunding);

            if (response != null && response.Status == true)
            {

                return Ok(response);

            }

            return BadRequest(response);
        }
    }
}
