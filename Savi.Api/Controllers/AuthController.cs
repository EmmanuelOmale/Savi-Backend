using Microsoft.AspNetCore.Mvc;
using Savi.Core.Interfaces;
using Savi.Data.DTO;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authenticationService;

        public AuthController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Register([FromBody] SignUpDto model)
        {

            try
            {
                var result = await _authenticationService.RegisterAsync(model);
                if (result.Succeeded)
                {
                    return Ok(result.Succeeded);
                }
                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
