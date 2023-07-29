using Microsoft.AspNetCore.Mvc;
using Savi.Core.Interfaces;
using Savi.Core.Services;
using Savi.Data.DTO;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IWalletService _walletService;
        public WalletController(IPaymentService paymentService, IWalletService walletService)
        {
            _paymentService = paymentService;
            _walletService = walletService;
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

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ResponseDto<WalletDTO>>> GetUserWalletAsync(string userId)
        {
            var response = await _walletService.GetUserWalletAsync(userId);

            if (response.StatusCode == 404)
            {
                return NotFound(new ResponseDto<WalletDTO>
                {
                    StatusCode = 404,
                    DisplayMessage = "User does not have a wallet."
                });
            }

            return Ok(response);
        }

        [HttpPost("transfer-funds")]
        public async Task<IActionResult> TransferFunds([FromBody] FundTransferRequestDto request)
        {
            var transferSuccess = await _walletService.TransferFundsAsync(request.SourceWalletId, request.DestinationWalletId, request.Amount);

            if (transferSuccess)
            {
                return Ok(new { Message = "Funds transferred successfully." });
            }
            else
            {
                return BadRequest(new { Message = "Failed to transfer funds. Please check the source wallet balance and destination wallet." });
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserTransactions(string userId)
        {
            var transactions = _walletService.GetUserTransactions(userId);
            return Ok(transactions);
        }

    }
}
