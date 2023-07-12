using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Savi.Core.Interfaces;
using Savi.Data.DTO;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KYCController : ControllerBase
    {
        private readonly IKYCService _kycService;

        public KYCController(IKYCService kycService)
        {
            _kycService = kycService;
        }

        [HttpPost("Verify")]

        public  Task<APIResponse> Verify([FromForm]KYC kyc)
        {
            var response = _kycService.VerifyUser(kyc);
            if (response == null)
            {
                var notFoundResponse = new APIResponse
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = false,
                    Message = "Bvn verification unsuccessful"
				};
                return Task.FromResult<APIResponse>(notFoundResponse);
            }
           

			var success = new APIResponse
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                IsSuccess = true,
                Message = "Bvn verification successful",
                
			};

            return Task.FromResult<APIResponse>(success);
        }
    }
}
